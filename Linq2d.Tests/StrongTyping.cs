using System;
using System.Collections.Generic;
using System.Text;
namespace Linq2d.Tests
{

    /// <summary>
    ///  This is not an XUnit test - it is supposed to verify the compile-time support for the expressions. 
    /// </summary>
    class StrongTyping
    {
        public void Test3ArraySelect()
        {
            var data = ArrayHelper.InitAll(10, 42);
            var q1 = from d in data select d+0;

            var q2 = from d1 in data
                     from d2 in data
                     select d1 + d2;

            var q3 = from d1 in data
                     from d2 in data
                     from d3 in data
                     select d1 + d2 + d3;

            var q4 = from d1 in data
                     from d2 in data
                     from d3 in data
                     from d4 in data
                     select d1 + d2 + d3 + d4;

        }
    }
}
