using System;
using Xunit;

namespace Linq2d.Tests
{
	public class Coverage
	{
		[Fact]
		public void CoverageTest1arg1result()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x in source
				select x+0;
			var r = q.ToArray();
			TestHelper.AssertEqual(source, r);
		}

		[Fact]
		public void CoverageTest1arg2results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x in source
				select ValueTuple.Create(x+0, x+0);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(source, r1);
			TestHelper.AssertEqual(source, r2);
		}
		#region 1 result
		[Fact]
		public void Test1Arg1Result()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select 1;
			var r = q.ToArray();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r);
		}
		[Fact]
		public void Test2Args1Result()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select 1;
			var r = q.ToArray();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r);
		}
		[Fact]
		public void Test3Args1Result()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select 1;
			var r = q.ToArray();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r);
		}
		[Fact]
		public void Test4Args1Result()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select 1;
			var r = q.ToArray();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r);
		}
		#endregion

		#region 2 results
		[Fact]
		public void Test1Arg2Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select ValueTuple.Create(1, 2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
		}
		[Fact]
		public void Test2Args2Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select ValueTuple.Create(1, 2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
		}
		[Fact]
		public void Test3Args2Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select ValueTuple.Create(1, 2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
		}
		[Fact]
		public void Test4Args2Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select ValueTuple.Create(1, 2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
		}
		#endregion

		#region 3 results
		[Fact]
		public void Test1Arg3Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select ValueTuple.Create(1, 2, 3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
		}
		[Fact]
		public void Test2Args3Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select ValueTuple.Create(1, 2, 3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
		}
		[Fact]
		public void Test3Args3Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select ValueTuple.Create(1, 2, 3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
		}
		[Fact]
		public void Test4Args3Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select ValueTuple.Create(1, 2, 3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
		}
		#endregion

		#region 4 results
		[Fact]
		public void Test1Arg4Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select ValueTuple.Create(1, 2, 3, 4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 4), r4);
		}
		[Fact]
		public void Test2Args4Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select ValueTuple.Create(1, 2, 3, 4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 4), r4);
		}
		[Fact]
		public void Test3Args4Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select ValueTuple.Create(1, 2, 3, 4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 4), r4);
		}
		[Fact]
		public void Test4Args4Results()
		{
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select ValueTuple.Create(1, 2, 3, 4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 1), r1);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 2), r2);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 3), r3);
			TestHelper.AssertEqual(ArrayHelper.InitAll(5, 5, 4), r4);
		}
		#endregion

	}
}
