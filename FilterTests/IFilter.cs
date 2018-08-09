using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Processing2d;
using System.Text;
using System.Threading.Tasks;

namespace FilterTests
{
    public interface IFilter<T>
    {
        void Initialize(T[,] data); 
    }
    public interface IWrapperFilter<T>: IFilter<T>
    {
        IArray2d<T> C4();
        IArray2d<T> C8();
    }
    public interface IArrayFilter<T>: IFilter<T>
    {
        T[,] C4();
        T[,] C8();
    }
}
