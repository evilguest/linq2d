using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Linq2d.CodeGen
{
    public class DynamicMethodBase
    {
        public Type[] ParameterTypes { get; }
        public Type ReturnType { get; }
        internal DynamicMethodBase(string name, bool saveAssembly, Type returnType, params Type[] parameterTypes)
        {
            ParameterTypes = parameterTypes;
            ReturnType = returnType;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            name = string.Concat(name.Split(Path.GetInvalidFileNameChars()));

            if (saveAssembly)
            {

                var a = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName
                {
                    Name = Assembly.GetCallingAssembly().GetName().Name + "." + name
                }, AssemblyBuilderAccess.RunAndCollect);

                ModuleBuilder m = a.DefineDynamicModule(a.GetName().Name + ".dll");

                var t = m.DefineType(name);
                _emiMethod = t.DefineMethod("Transform", MethodAttributes.Public | MethodAttributes.Static, returnType, parameterTypes);
            }
            else
            {
                _dynMethod = new DynamicMethod(name, returnType,
                    parameterTypes, restrictedSkipVisibility: true);
            }

        }

        private readonly DynamicMethod _dynMethod;
        private readonly MethodBuilder _emiMethod;
        public void GenerateIL(Action<ILGenerator> generate)
        {
            if (_dynMethod != null)
                generate(_dynMethod.GetILGenerator());
            else
                generate(_emiMethod.GetILGenerator());
        }

        protected Delegate CreateDelegate(Type delegateType)
        {
            if (_dynMethod != null)
                return _dynMethod.CreateDelegate(delegateType);
            else
            {
                var tb = _emiMethod.DeclaringType as TypeBuilder;
                var t = tb.CreateType();
                var assembly = t.Assembly;

                try
                {
                    var generator = new Lokad.ILPack.AssemblyGenerator();

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Dynamic");
                    Directory.CreateDirectory(path);
                    generator.GenerateAssembly(assembly, Path.Combine(path, assembly.GetName().Name + ".dll"));
                }
                catch
                {
                    // we don't care if the assembly could not be created - it is for debug purposes anyway.
                }
                return t.GetMethod(_emiMethod.Name).CreateDelegate(delegateType);
            }

        }

    }

    public class DynamicMethod<D> : DynamicMethodBase
        where D : Delegate
    {
        public static Type GetReturnType() => typeof(D).GetMethod("Invoke").ReturnType;
        public static Type[] GetParameterTypes() => (from p in typeof(D).GetMethod("Invoke").GetParameters() select p.ParameterType).ToArray();
        public DynamicMethod(string name, bool saveAssembly = false) : base(name, saveAssembly, GetReturnType(), GetParameterTypes())
        {
        }


        public D CreateDelegate()
        {
            return (D)CreateDelegate(typeof(D));
        }
    }
}
