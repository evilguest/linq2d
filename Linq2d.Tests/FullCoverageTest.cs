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
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select 0*x1;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			q = 
				from x1 in source.With(0)
				select 0*x1;
			r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source.With(0)
				let y = 0*x1
				select 0*y + 0*x1;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test1Arg1ResultWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test2Args1Result()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select 0*x1 + 0*x2;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			q = 
				from x1 in source
				from x2 in source.With(0)
				select 0*x1 + 0*x2;
			r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select 0*y + 0*x1 + 0*x2;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test2Args1ResultWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test3Args1Result()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select 0*x1 + 0*x2 + 0*x3;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3;
			r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select 0*y + 0*x1 + 0*x2 + 0*x3;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test3Args1ResultWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test4Args1Result()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4;
			r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test4Args1ResultWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		#endregion

		#region 2 results
		[Fact]
		public void Test1Arg2Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select ValueTuple.Create(0*x1, 0*x1);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			q = 
				from x1 in source.With(0)
				select ValueTuple.Create(0*x1, 0*x1);
			(r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source.With(0)
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test1Arg2ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test1Arg2ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test2Args2Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			q = 
				from x1 in source
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2);
			(r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test2Args2ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test2Args2ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test3Args2Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			(r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test3Args2ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test3Args2ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test4Args2Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			(r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test4Args2ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test4Args2ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		#endregion

		#region 3 results
		[Fact]
		public void Test1Arg3Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select ValueTuple.Create(0*x1, 0*x1, 0*x1);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			q = 
				from x1 in source.With(0)
				select ValueTuple.Create(0*x1, 0*x1, 0*x1);
			(r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source.With(0)
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test1Arg3ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test2Args3Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			q = 
				from x1 in source
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			(r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test2Args3ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test3Args3Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			(r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test3Args3ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test4Args3Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			(r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test4Args3ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		#endregion

		#region 4 results
		[Fact]
		public void Test1Arg4Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				select ValueTuple.Create(0*x1, 0*x1, 0*x1, 0*x1);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			q = 
				from x1 in source.With(0)
				select ValueTuple.Create(0*x1, 0*x1, 0*x1, 0*x1);
			(r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source.With(0)
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test1Arg4ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test2Args4Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			q = 
				from x1 in source
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			(r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test2Args4ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test3Args4Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			(r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test3Args4ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test4Args4Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			(r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test4Args4ResultsWithRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		#endregion

	}
}
