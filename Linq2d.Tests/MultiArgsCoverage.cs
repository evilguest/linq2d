using System;
using Xunit;

namespace Linq2d.Tests
{
	public class MultiArgsCoverage
	{
		#region 1 result

		[Fact]
		public void Test2ArgsWrapped1Result()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from x2 in source
				select 0*x1 + 0*x2;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			q = 
				from x1 in source.With(0)
				from x2 in source.With(0)
				select 0*x1 + 0*x2;
			r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source.With(0)
				from x2 in source
				let y = 0*x1 + 0*x2
				select 0*y + 0*x1 + 0*x2;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


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
		}

		[Fact]
		public void Test1Arg1ResultWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				select 0*y + 0*x1;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		[Fact]
		public void Test1Arg1ResultWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				let y = 0*x1
				select 0*y + 0*x1;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test1Arg1ResultWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				select 0*x1;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source.With(0)
				let y = 0*x1
				select 0*y + 0*x1;
			r = q2.ToArray();
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
		}

		[Fact]
		public void Test2Args1ResultWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				select 0*y + 0*x1 + 0*x2;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test2Args1ResultVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source
				select 0*y + 0*x1 + 0*x2;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test2Args1ResultVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source.With(0)
				select 0*y + 0*x1 + 0*x2;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test2Args1ResultWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select 0*y + 0*x1 + 0*x2;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test2Args1ResultWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				select 0*x1 + 0*x2;
			var r = q.ToArray();
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
		}

		[Fact]
		public void Test3Args1ResultWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				select 0*y + 0*x1 + 0*x2 + 0*x3;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test3Args1ResultVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test3Args1ResultVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test3Args1ResultWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select 0*y + 0*x1 + 0*x2 + 0*x3;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test3Args1ResultWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3;
			var r = q.ToArray();
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
		}

		[Fact]
		public void Test4Args1ResultWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test4Args1ResultVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test4Args1ResultVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test4Args1ResultWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test4Args1ResultWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4;
			var r = q.ToArray();
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
		public void Test5Args1Result()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test5Args1ResultWithVariable()
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
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test5Args1ResultVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test5Args1ResultVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test5Args1ResultWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test5Args1ResultWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test6Args1Result()
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
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test6Args1ResultWithVariable()
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
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test6Args1ResultVariableWith()
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
				from x6 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test6Args1ResultVariableWithWrapped()
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
				from x6 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test6Args1ResultWrappedWithVariable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test6Args1ResultWrapped()
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
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}

		#endregion

		#region 2 results

		[Fact]
		public void Test2ArgsWrapped2Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from x2 in source
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			q = 
				from x1 in source.With(0)
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2);
			(r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source.With(0)
				from x2 in source
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


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
		}

		[Fact]
		public void Test1Arg2ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		[Fact]
		public void Test1Arg2ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test1Arg2ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				select ValueTuple.Create(0*x1, 0*x1);
			var (r1, r2) = q.ToArrays();
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
		}

		[Fact]
		public void Test2Args2ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test2Args2ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test2Args2ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test2Args2ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test2Args2ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2) = q.ToArrays();
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
		}

		[Fact]
		public void Test3Args2ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test3Args2ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test3Args2ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test3Args2ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test3Args2ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			var (r1, r2) = q.ToArrays();
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
		}

		[Fact]
		public void Test4Args2ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test4Args2ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test4Args2ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test4Args2ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test4Args2ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2) = q.ToArrays();
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
		public void Test5Args2Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test5Args2ResultsWithVariable()
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
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test5Args2ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test5Args2ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test5Args2ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test5Args2ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test6Args2Results()
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
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test6Args2ResultsWithVariable()
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
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test6Args2ResultsVariableWith()
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
				from x6 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test6Args2ResultsVariableWithWrapped()
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
				from x6 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test6Args2ResultsWrappedWithVariable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test6Args2ResultsWrapped()
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
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}

		#endregion

		#region 3 results

		[Fact]
		public void Test2ArgsWrapped3Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from x2 in source
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			q = 
				from x1 in source.With(0)
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			(r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source.With(0)
				from x2 in source
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


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
		}

		[Fact]
		public void Test1Arg3ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		[Fact]
		public void Test1Arg3ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test1Arg3ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				select ValueTuple.Create(0*x1, 0*x1, 0*x1);
			var (r1, r2, r3) = q.ToArrays();
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
		}

		[Fact]
		public void Test2Args3ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test2Args3ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test2Args3ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test2Args3ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test2Args3ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2, r3) = q.ToArrays();
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
		}

		[Fact]
		public void Test3Args3ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test3Args3ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test3Args3ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test3Args3ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test3Args3ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3) = q.ToArrays();
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
		}

		[Fact]
		public void Test4Args3ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test4Args3ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test4Args3ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test4Args3ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test4Args3ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3) = q.ToArrays();
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
		public void Test5Args3Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test5Args3ResultsWithVariable()
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
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test5Args3ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test5Args3ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test5Args3ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test5Args3ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test6Args3Results()
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
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test6Args3ResultsWithVariable()
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
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test6Args3ResultsVariableWith()
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
				from x6 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test6Args3ResultsVariableWithWrapped()
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
				from x6 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test6Args3ResultsWrappedWithVariable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test6Args3ResultsWrapped()
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
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}

		#endregion

		#region 4 results

		[Fact]
		public void Test2ArgsWrapped4Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				from x2 in source
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			q = 
				from x1 in source.With(0)
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			(r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source.With(0)
				from x2 in source
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


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
		}

		[Fact]
		public void Test1Arg4ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		[Fact]
		public void Test1Arg4ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				let y = 0*x1
				select ValueTuple.Create(0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1, 0*y + 0*x1);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test1Arg4ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source.With(0)
				select ValueTuple.Create(0*x1, 0*x1, 0*x1, 0*x1);
			var (r1, r2, r3, r4) = q.ToArrays();
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
		}

		[Fact]
		public void Test2Args4ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test2Args4ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test2Args4ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				let y = 0*x1
				from x2 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test2Args4ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				let y = 0*x1 + 0*x2
				select ValueTuple.Create(0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2, 0*y + 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test2Args4ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2, 0*x1 + 0*x2);
			var (r1, r2, r3, r4) = q.ToArrays();
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
		}

		[Fact]
		public void Test3Args4ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test3Args4ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test3Args4ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				let y = 0*x1 + 0*x2
				from x3 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test3Args4ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3, 0*y + 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test3Args4ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3, 0*x1 + 0*x2 + 0*x3);
			var (r1, r2, r3, r4) = q.ToArrays();
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
		}

		[Fact]
		public void Test4Args4ResultsWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test4Args4ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test4Args4ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				let y = 0*x1 + 0*x2 + 0*x3
				from x4 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test4Args4ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test4Args4ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4, 0*x1 + 0*x2 + 0*x3 + 0*x4);
			var (r1, r2, r3, r4) = q.ToArrays();
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
		public void Test5Args4Results()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test5Args4ResultsWithVariable()
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
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test5Args4ResultsVariableWith()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test5Args4ResultsVariableWithWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4
				from x5 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test5Args4ResultsWrappedWithVariable()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test5Args4ResultsWrapped()
		{
			var expect = ArrayHelper.InitAll(5, 5, 0);
			var source = ArrayHelper.InitAllRand(5, 5, 42);
			var q = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test6Args4Results()
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
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test6Args4ResultsWithVariable()
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
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test6Args4ResultsVariableWith()
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
				from x6 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test6Args4ResultsVariableWithWrapped()
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
				from x6 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test6Args4ResultsWrappedWithVariable()
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
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test6Args4ResultsWrapped()
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
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		#endregion

	}
}
