namespace Linq2d
{

    public interface IArray<R>
    {
        R[,] ToArray();
    }

    public interface IArray<R1, R2>
    {
        (R1[,], R2[,]) ToArrays();
    }

    public interface IArray<R1, R2, R3>
    {
        (R1[,], R2[,], R3[,]) ToArrays();
    }
}
