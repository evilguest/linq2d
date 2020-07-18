using System;
using System.Collections.Generic;
using System.Text;

namespace Linq2d
{
    public class Result<R>:Result
    {
        public R InitValue { get; }
        public Result(R initValue) => InitValue = initValue;
    }

    public class Result
    {
        public static Result<R> SubstBy<R>(R substValue) => new Result<R>(substValue);
    }
}
