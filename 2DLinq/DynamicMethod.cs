using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Processing2d
{
    public class DynamicMethod<D>
        where D: Delegate
    {
        private readonly DynamicMethod _method;

        internal DynamicMethod(string name, Module module=null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            module = module ?? typeof(DynamicMethod<D>).Module;

            MethodInfo invoke = typeof(D).GetMethod("Invoke");

            var parameterTypes = (from p in invoke.GetParameters() select p.ParameterType).ToArray();

            _method = new DynamicMethod(name, invoke.ReturnType,
                parameterTypes, restrictedSkipVisibility: true);
        }

        public ILGenerator GetILGenerator() => _method.GetILGenerator();

        public D CreateDelegate()
        {
            return (D)_method.CreateDelegate(typeof(D));
        }
    }
}
