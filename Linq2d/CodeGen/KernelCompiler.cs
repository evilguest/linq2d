using Linq.Expressions.Deconstruct;
using Linq2d.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace Linq2d.CodeGen
{
    abstract class KernelCompiler : ExpressionVisitor
    {
        private readonly ILGenerator _generator;
        private readonly Dictionary<LabelTarget, Label> _labelMap = new Dictionary<LabelTarget, Label>();
        private readonly Expression _width;
        protected ILGenerator Generator { get => _generator; }
        protected KernelCompiler(ILGenerator generator, Expression width)
        {
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _width = width ?? throw new ArgumentNullException(nameof(width));
        }

        public IDictionary<ParameterExpression, LocalBuilder> VariableMap { get; } = new Dictionary<ParameterExpression, LocalBuilder>();

        public void Compile(Expression expr) => Visit(expr);
        public override Expression Visit(Expression node)
        {
            if (node == null)
                return node;

            switch (node.NodeType)
            {
                case ExpressionType.Assign: return VisitAssign((BinaryExpression)node);
                default: return base.Visit(node);
            }
        }

        protected virtual Expression VisitAssign(BinaryExpression node)
        {
            Visit(node.Right);
            _generator.Stloc(VariableMap[(ParameterExpression)node.Left]);
            return node;

        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            foreach (var v in node.Variables)
            {
                if (!VariableMap.ContainsKey(v))
                    VariableMap[v] = _generator.DeclareLocal(v.Type);
            }
            Visit(node.Expressions); // skip variable visiting - already handled
            return node;
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            if (!_labelMap.ContainsKey(node.Target))
                _labelMap[node.Target] = _generator.DefineLabel();
            _generator.Br(_labelMap[node.Target]);
            return base.VisitGoto(node);
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            _generator.MarkLabel(_labelMap[node]);
            return base.VisitLabelTarget(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            //if(!VariableMap.ContainsKey(node))
            //    VariableMap[node] = _generator.DeclareLocal(node.Type);
            _generator.Ldloc(VariableMap[node]);
            return base.VisitParameter(node);
        }


        protected override Expression VisitConditional(ConditionalExpression node)
        {
            var falseLoc = Generator.DefineLabel();
            var endLoc = Generator.DefineLabel();

            //if(node.Test is BinaryExpression be)
            //{
            //    if(be.NodeType == ExpressionType.GreaterThan)
            //    {
            //        base.Visit(node.Test);
            //        Generator.Emit(OpCodes.Ble, falseLoc);
            //        Visit(node.IfTrue);
            //        Generator.Br(endLoc);
            //        Generator.MarkLabel(falseLoc);
            //        Visit(node.IfFalse);
            //        Generator.MarkLabel(endLoc);
            //        return node;
            //    }
            //}
            Visit(node.Test);
            Generator.Emit(OpCodes.Brfalse, falseLoc);
            Visit(node.IfTrue);
            Generator.Emit(OpCodes.Br, endLoc);
            Generator.MarkLabel(falseLoc);
            Visit(node.IfFalse);
            Generator.MarkLabel(endLoc);
            return node;
        }

        private static T[] Repeat<T>(T element, int n)
        {
            var r = new T[n];
            for (var i = 0; i < n; i++) r[i] = element;
            return r;
        }
        private static Dictionary<Type, OpCode> _arrayStoreCodes = new Dictionary<Type, OpCode>()
        {
            {typeof(byte),   OpCodes.Stelem_I1},
            {typeof(sbyte),  OpCodes.Stelem_I1},
            {typeof(int),    OpCodes.Stelem_I4},
            {typeof(uint),   OpCodes.Stelem_I4},
            {typeof(short),  OpCodes.Stelem_I2},
            {typeof(ushort), OpCodes.Stelem_I2},
            {typeof(long),   OpCodes.Stelem_I8},
            {typeof(ulong),  OpCodes.Stelem_I8},
            {typeof(float),  OpCodes.Stelem_R4},
            {typeof(double), OpCodes.Stelem_R8}
        };
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.NewArrayBounds:
                    Visit(node.Expressions);
                    Generator.Emit(OpCodes.Newobj, node.Type.GetConstructor(Repeat(typeof(int), node.Expressions.Count)));
                    break;

                case ExpressionType.NewArrayInit:
                    var elementType = node.Type.GetElementType();
                    var storeOpCode = _arrayStoreCodes.ContainsKey(elementType)
                        ? _arrayStoreCodes[elementType]
                        : (elementType.IsClass ? OpCodes.Stelem_Ref : OpCodes.Stelem);

                    Generator.Emit(OpCodes.Ldc_I4, node.Expressions.Count);
                    Generator.Emit(OpCodes.Newarr, elementType);
                    for (var i = 0; i < node.Expressions.Count; i++)
                    {
                        Generator.Emit(OpCodes.Dup); // array reference
                        Generator.Emit(OpCodes.Ldc_I4, i); // index
                        Visit(node.Expressions[i]); // initializer expression
                        if (storeOpCode == OpCodes.Stelem)
                            Generator.Emit(storeOpCode, elementType);
                        else
                            Generator.Emit(storeOpCode);
                    }
                    break;
            }

            return node;
        
        }
        public void Load2dPointerOffset(Type type, Expression x, Expression y)
        {
            var s = (int)typeof(Unsafe).GetMethod("SizeOf").MakeGenericMethod(type).Invoke(null, null); // Marshal.SizeOf(t);

            var offset = Arithmetic.Simplify(Expression.Add(Expression.Multiply(x, _width), y), Ranges.No);
            if (offset is ConstantExpression ce && 0.Equals(ce.Value))
                return;
            if (s > 1)
                offset = Expression.Multiply(offset, Expression.Constant(s));

            Visit(offset);
            Generator.Add();          
        }
        protected void LoadItemAddress(IndexExpression node)
        {
            Visit(node.Object);        // pArray
            Load2dPointerOffset(node.Type, node.Arguments[0], node.Arguments[1]);
        }

    }
}
