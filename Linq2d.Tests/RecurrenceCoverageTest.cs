using System;
using Xunit;

namespace Linq2d.Tests
{
	public class RecurrenceCoverage
	{
		#region 1 result
		#region 1 arument

		[Fact]
		public void Test1Arg1ResultWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1
				select 0*z1[-1, -1] + 0*y + 0*x1;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test1Arg1ResultWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*y + 0*x1;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test1Arg1ResultWithRecurrence1Array()
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
		public void Test1Arg1ResultWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		#endregion //n
		#region 2 aruments

		[Fact]
		public void Test2Args1ResultWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test2Args1ResultWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test2Args1ResultWithRecurrence1Array()
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
		public void Test2Args1ResultWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		#endregion //n
		#region 3 aruments

		[Fact]
		public void Test3Args1ResultWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test3Args1ResultWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test3Args1ResultWithRecurrence1Array()
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
		public void Test3Args1ResultWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		#endregion //n
		#region 4 aruments

		[Fact]
		public void Test4Args1ResultWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test4Args1ResultWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test4Args1ResultWithRecurrence1Array()
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
		[Fact]
		public void Test4Args1ResultWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		#endregion //n
		#region 5 aruments

		[Fact]
		public void Test5Args1ResultWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test5Args1ResultWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test5Args1ResultWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test5Args1ResultWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		#endregion //n
		#region 6 aruments

		[Fact]
		public void Test6Args1ResultWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test6Args1ResultWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test6Args1ResultWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test6Args1ResultWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				select 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;

			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		#endregion //n
		#endregion //k

		#region 2 results
		#region 1 arument

		[Fact]
		public void Test1Arg2ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test1Arg2ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test1Arg2ResultsWithRecurrence1Array()
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
		public void Test1Arg2ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test1Arg2ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test1Arg2ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test1Arg2ResultsWithRecurrence2Array()
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
		public void Test1Arg2ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		#endregion //n
		#region 2 aruments

		[Fact]
		public void Test2Args2ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test2Args2ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test2Args2ResultsWithRecurrence1Array()
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
		public void Test2Args2ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test2Args2ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test2Args2ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test2Args2ResultsWithRecurrence2Array()
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
		public void Test2Args2ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		#endregion //n
		#region 3 aruments

		[Fact]
		public void Test3Args2ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test3Args2ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test3Args2ResultsWithRecurrence1Array()
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
		public void Test3Args2ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test3Args2ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test3Args2ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test3Args2ResultsWithRecurrence2Array()
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
		public void Test3Args2ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		#endregion //n
		#region 4 aruments

		[Fact]
		public void Test4Args2ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test4Args2ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test4Args2ResultsWithRecurrence1Array()
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
		public void Test4Args2ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test4Args2ResultsWithRecurrence2Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test4Args2ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test4Args2ResultsWithRecurrence2Array()
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
		[Fact]
		public void Test4Args2ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		#endregion //n
		#region 5 aruments

		[Fact]
		public void Test5Args2ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithRecurrence2Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		#endregion //n
		#region 6 aruments

		[Fact]
		public void Test6Args2ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithRecurrence2Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		#endregion //n
		#endregion //k

		#region 3 results
		#region 1 arument

		[Fact]
		public void Test1Arg3ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence1Array()
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
		public void Test1Arg3ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence2Array()
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
		public void Test1Arg3ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test1Arg3ResultsWithRecurrence3Array()
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
		public void Test1Arg3ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		#endregion //n
		#region 2 aruments

		[Fact]
		public void Test2Args3ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence1Array()
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
		public void Test2Args3ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence2Array()
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
		public void Test2Args3ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsWithRecurrence3Array()
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
		public void Test2Args3ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		#endregion //n
		#region 3 aruments

		[Fact]
		public void Test3Args3ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence1Array()
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
		public void Test3Args3ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence2Array()
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
		public void Test3Args3ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence3Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsWithRecurrence3Array()
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
		public void Test3Args3ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		#endregion //n
		#region 4 aruments

		[Fact]
		public void Test4Args3ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence1Array()
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
		public void Test4Args3ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence2Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence2Array()
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
		public void Test4Args3ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence3Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsWithRecurrence3Array()
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
		[Fact]
		public void Test4Args3ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		#endregion //n
		#region 5 aruments

		[Fact]
		public void Test5Args3ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence2Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence3Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		#endregion //n
		#region 6 aruments

		[Fact]
		public void Test6Args3ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence2Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence3Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		#endregion //n
		#endregion //k

		#region 4 results
		#region 1 arument

		[Fact]
		public void Test1Arg4ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence1Array()
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
		public void Test1Arg4ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1, 0*z1[-1, -1] + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence2Array()
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
		public void Test1Arg4ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
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
		public void Test1Arg4ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence3Array()
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
		public void Test1Arg4ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
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
		public void Test1Arg4ResultsWithRecurrence4Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				let y = 0*x1
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithVariableRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test1Arg4ResultsWithRecurrence4Array()
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
		public void Test1Arg4ResultsWithRecurrence4ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
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
		#endregion //n
		#region 2 aruments

		[Fact]
		public void Test2Args4ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence1Array()
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
		public void Test2Args4ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence2Array()
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
		public void Test2Args4ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
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
		public void Test2Args4ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence3Array()
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
		public void Test2Args4ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
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
		public void Test2Args4ResultsWithRecurrence4Variable()
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
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithVariableRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsWithRecurrence4Array()
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
		public void Test2Args4ResultsWithRecurrence4ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
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
		#endregion //n
		#region 3 aruments

		[Fact]
		public void Test3Args4ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence1Array()
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
		public void Test3Args4ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence2Array()
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
		public void Test3Args4ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
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
		public void Test3Args4ResultsWithRecurrence3Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence3Array()
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
		public void Test3Args4ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
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
		public void Test3Args4ResultsWithRecurrence4Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithVariableRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsWithRecurrence4Array()
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
		public void Test3Args4ResultsWithRecurrence4ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
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
		#endregion //n
		#region 4 aruments

		[Fact]
		public void Test4Args4ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence1Array()
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
		public void Test4Args4ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence2Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence2Array()
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
		public void Test4Args4ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
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
		public void Test4Args4ResultsWithRecurrence3Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence3Array()
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
		public void Test4Args4ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
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
		public void Test4Args4ResultsWithRecurrence4Variable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithVariableRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsWithRecurrence4Array()
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
		[Fact]
		public void Test4Args4ResultsWithRecurrence4ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
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
		#endregion //n
		#region 5 aruments

		[Fact]
		public void Test5Args4ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence2Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence3Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence4Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithVariableRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence4Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsWithRecurrence4ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		#endregion //n
		#region 6 aruments

		[Fact]
		public void Test6Args4ResultsWithRecurrence1Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithVariableRecurrence1()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence1Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence1ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence2Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithVariableRecurrence2()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence2Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence2ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence3Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithVariableRecurrence3()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence3Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence3ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence4Variable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithVariableRecurrence4()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence4Array()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsWithRecurrence4ArraySource()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				from z1 in Result.InitWith(0)
				from z2 in Result.InitWith(0)
				from z3 in Result.InitWith(0)
				from z4 in Result.InitWith(0)
				select ValueTuple.Create(0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*z1[-1, -1] + 0*z2[-1, -1] + 0*z3[-1, -1] + 0*z4[-1, -1] + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);

			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		#endregion //n
		#endregion //k

	}
}
