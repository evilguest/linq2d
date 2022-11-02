namespace Linq2d
{
    public static partial class Array2d
    {
        #region control
        public static bool TryVectorize { get; set; } = true;
        public static bool EliminateCommonSubexpressions { get; set; } = true;
        public static bool PoolCSEVariables { get; set; } = true;
        public static bool SaveDynamicCode { get; set; } = false;
        public static bool MoveLoopInvariants {get; set; } = true;
        #endregion

        #region window functions
//        public static (Cell<T> tl, Cell<T> tr, Cell<T> bl, Cell<T> br) Window<T>(this Cell<T> cell, int size)
//            =>(cell, cell, cell, cell);
//        public static int Area<T>(this (Cell<T> tl, Cell<T> tr, Cell<T> bl, Cell<T> br) window)=>(window.br.X - window.tl.X) * (window.br.Y - window.tl.Y);
        
        #endregion
        public static ArraySource<T> With<T>(this T[,] source, T initValue) => new ArraySource<T>(source, initValue);
        public static ArraySource<T> With<T>(this T[,] source, OutOfBoundsStrategy<T> strategy) => new ArraySource<T>(source, strategy);
        public static ArraySource<T> With<T>(this T[,] source, OutOfBoundsStrategyUntyped strategy) => new ArraySource<T>(source, strategy);
    }

}