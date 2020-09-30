namespace Linq2d
{
    public class Result<R> 
    {
        public R InitValue { get; }
        public Result(R initValue) => InitValue = initValue;
    }

    public static class Result
    {
        public static Result<R> InitWith<R>(R substValue) => new Result<R>(substValue);
    }
}
