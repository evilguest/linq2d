using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Linq2d.CodeGen
{

    public abstract class MethodBuilder<D> where D : Delegate 
    {
        public abstract ILGenerator GetIlGenerator();
        public abstract D CreateDelegate();
        public static MethodBuilder<D> Create(bool saveAssembly, string name) =>
            saveAssembly 
                ? new SavingMethodBuilder<D>(name) 
                : new DynamicMethodBuilder<D>(name);

        internal static readonly Type ReturnType = typeof(D).GetMethod("Invoke").ReturnType;
        internal static readonly Type[] ParameterTypes = (from p in typeof(D).GetMethod("Invoke").GetParameters() select p.ParameterType).ToArray();
    }


    class DynamicMethodBuilder<D>: MethodBuilder<D> where D : Delegate 
    {
        private readonly DynamicMethod _dynMethod;
        public DynamicMethodBuilder(string name)
            => _dynMethod = new DynamicMethod(name, ReturnType, ParameterTypes, restrictedSkipVisibility: true);

        public override D CreateDelegate() => (D)_dynMethod.CreateDelegate(typeof(D));

        public override ILGenerator GetIlGenerator() => _dynMethod.GetILGenerator();
    }

    class SavingMethodBuilder<D>: MethodBuilder<D> where D: Delegate 
    {
        private readonly MethodBuilder _emiMethod;
        public SavingMethodBuilder(string name) 
        {
            var a = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName { Name = Assembly.GetCallingAssembly().GetName().Name + "." + name }, AssemblyBuilderAccess.RunAndCollect);

            _emiMethod = a
                .DefineDynamicModule(a.GetName().Name + ".dll")
                .DefineType(name, TypeAttributes.Public | TypeAttributes.Sealed)
                .DefineMethod("Transform", MethodAttributes.Public | MethodAttributes.Static, ReturnType, ParameterTypes);
        }

        public override D CreateDelegate()
        {
            var tb = _emiMethod.DeclaringType as TypeBuilder;
            var t = tb.CreateType();
            var assembly = t.Assembly;

            var generator = new Lokad.ILPack.AssemblyGenerator();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Dynamic");
            Directory.CreateDirectory(path);
            generator.GenerateAssembly(assembly, Path.Combine(path, assembly.GetName().Name + ".dll"));
            return (D)t.GetMethod(_emiMethod.Name).CreateDelegate(typeof(D));
        }


        public override ILGenerator GetIlGenerator() => _emiMethod.GetILGenerator();
    }
}
