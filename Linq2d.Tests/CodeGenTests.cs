using Xunit;
using Linq2d.CodeGen;
using System;

namespace Linq2d.Tests
{
    public class CodeGenTests
    {
        [Fact]
        public void TestTypeHelperIsNumeric()
        {
            Assert.True(typeof(int).IsNumeric());
            Assert.True(typeof(uint).IsNumeric());
            Assert.True(typeof(long).IsNumeric());
            Assert.True(typeof(ulong).IsNumeric());
            Assert.True(typeof(byte).IsNumeric());
            Assert.True(typeof(sbyte).IsNumeric());
            Assert.True(typeof(short).IsNumeric());
            Assert.True(typeof(ushort).IsNumeric());
            Assert.True(typeof(decimal).IsNumeric());
            Assert.True(typeof(int?).IsNumeric());
            Assert.True(typeof(uint?).IsNumeric());
            Assert.True(typeof(long?).IsNumeric());
            Assert.True(typeof(ulong?).IsNumeric());
            Assert.True(typeof(byte?).IsNumeric());
            Assert.True(typeof(sbyte?).IsNumeric());
            Assert.True(typeof(short?).IsNumeric());
            Assert.True(typeof(ushort?).IsNumeric());
            Assert.True(typeof(decimal?).IsNumeric());

            Assert.False(typeof(string).IsNumeric());
            Assert.False(typeof(object).IsNumeric());
            Assert.False(typeof(DateTime).IsNumeric());

        }
    }
}
