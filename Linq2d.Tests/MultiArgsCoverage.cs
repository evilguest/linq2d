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


		[Fact]
		public void Test7Args1Result()
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
				from x7 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test7Args1ResultWithVariable()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test7Args1ResultVariableWith()
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
				from x7 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test7Args1ResultVariableWithWrapped()
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
				from x7 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test7Args1ResultWrappedWithVariable()
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
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test7Args1ResultWrapped()
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
				from x7 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test8Args1Result()
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
				from x7 in source
				from x8 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test8Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test8Args1ResultVariableWith()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test8Args1ResultVariableWithWrapped()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test8Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test8Args1ResultWrapped()
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
				from x7 in source
				from x8 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test9Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test9Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test9Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test9Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test9Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test9Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test10Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test10Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test10Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test10Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test10Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test10Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test11Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test11Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test11Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test11Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test11Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test11Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test12Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test12Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test12Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test12Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test12Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test12Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test13Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test13Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test13Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test13Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test13Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test13Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test14Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test14Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test14Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test14Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test14Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test14Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test15Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test15Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test15Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test15Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test15Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test15Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15;
			r = q2.ToArray();
			TestHelper.AssertEqual(expect, r);

		}


		[Fact]
		public void Test16Args1Result()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test16Args1ResultWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}
		[Fact]
		public void Test16Args1ResultVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}
		[Fact]
		public void Test16Args1ResultVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source.With(0)
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
		}

		[Fact]
		public void Test16Args1ResultWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);

		}		[Fact]
		public void Test16Args1ResultWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				select 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
			var r = q.ToArray();
			TestHelper.AssertEqual(expect, r);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16;
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


		[Fact]
		public void Test7Args2Results()
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
				from x7 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test7Args2ResultsWithVariable()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test7Args2ResultsVariableWith()
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
				from x7 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test7Args2ResultsVariableWithWrapped()
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
				from x7 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test7Args2ResultsWrappedWithVariable()
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
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test7Args2ResultsWrapped()
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
				from x7 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test8Args2Results()
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
				from x7 in source
				from x8 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test8Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test8Args2ResultsVariableWith()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test8Args2ResultsVariableWithWrapped()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test8Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test8Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test9Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test9Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test9Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test9Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test9Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test9Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test10Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test10Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test10Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test10Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test10Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test10Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test11Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test11Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test11Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test11Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test11Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test11Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test12Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test12Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test12Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test12Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test12Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test12Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test13Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test13Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test13Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test13Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test13Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test13Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test14Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test14Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test14Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test14Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test14Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test14Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test15Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test15Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test15Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test15Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test15Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test15Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			(r1, r2) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}


		[Fact]
		public void Test16Args2Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test16Args2ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}
		[Fact]
		public void Test16Args2ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}
		[Fact]
		public void Test16Args2ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
		}

		[Fact]
		public void Test16Args2ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);

		}		[Fact]
		public void Test16Args2ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			var q2 = 
				from x1 in source
				from x2 in source
				from x3 in source
				from x4 in source
				from x5 in source
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
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


		[Fact]
		public void Test7Args3Results()
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
				from x7 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test7Args3ResultsWithVariable()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test7Args3ResultsVariableWith()
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
				from x7 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test7Args3ResultsVariableWithWrapped()
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
				from x7 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test7Args3ResultsWrappedWithVariable()
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
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test7Args3ResultsWrapped()
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
				from x7 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
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
				from x6 in source
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test8Args3Results()
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
				from x7 in source
				from x8 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test8Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test8Args3ResultsVariableWith()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test8Args3ResultsVariableWithWrapped()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test8Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test8Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
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
				from x6 in source
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test9Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test9Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test9Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test9Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test9Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test9Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test10Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test10Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test10Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test10Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test10Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test10Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test11Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test11Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test11Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test11Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test11Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test11Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test12Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test12Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test12Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test12Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test12Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test12Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test13Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test13Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test13Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test13Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test13Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test13Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test14Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test14Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test14Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test14Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test14Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test14Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test15Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test15Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test15Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test15Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test15Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test15Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			(r1, r2, r3) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}


		[Fact]
		public void Test16Args3Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test16Args3ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}
		[Fact]
		public void Test16Args3ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}
		[Fact]
		public void Test16Args3ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
		}

		[Fact]
		public void Test16Args3ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);

		}		[Fact]
		public void Test16Args3ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
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


		[Fact]
		public void Test7Args4Results()
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
				from x7 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test7Args4ResultsWithVariable()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test7Args4ResultsVariableWith()
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
				from x7 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test7Args4ResultsVariableWithWrapped()
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
				from x7 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test7Args4ResultsWrappedWithVariable()
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
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test7Args4ResultsWrapped()
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
				from x7 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
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
				from x6 in source
				from x7 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test8Args4Results()
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
				from x7 in source
				from x8 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test8Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test8Args4ResultsVariableWith()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test8Args4ResultsVariableWithWrapped()
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
				from x7 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7
				from x8 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test8Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test8Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
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
				from x6 in source
				from x7 in source
				from x8 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test9Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test9Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test9Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test9Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8
				from x9 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test9Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test9Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test10Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test10Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test10Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test10Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9
				from x10 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test10Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test10Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test11Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test11Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test11Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test11Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10
				from x11 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test11Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test11Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test12Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test12Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test12Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test12Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11
				from x12 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test12Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test12Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test13Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test13Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test13Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test13Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12
				from x13 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test13Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test13Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test14Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test14Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test14Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test14Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13
				from x14 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test14Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test14Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test15Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test15Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test15Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test15Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14
				from x15 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test15Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test15Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}


		[Fact]
		public void Test16Args4Results()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test16Args4ResultsWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}
		[Fact]
		public void Test16Args4ResultsVariableWith()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}
		[Fact]
		public void Test16Args4ResultsVariableWithWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15
				from x16 in source.With(0)
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);
		}

		[Fact]
		public void Test16Args4ResultsWrappedWithVariable()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			var (r1, r2, r3, r4) = q.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}		[Fact]
		public void Test16Args4ResultsWrapped()
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
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				select ValueTuple.Create(0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
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
				from x6 in source
				from x7 in source
				from x8 in source
				from x9 in source
				from x10 in source
				from x11 in source
				from x12 in source
				from x13 in source
				from x14 in source
				from x15 in source
				from x16 in source.With(0)
				let y = 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16
				select ValueTuple.Create(0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16, 0*y + 0*x1 + 0*x2 + 0*x3 + 0*x4 + 0*x5 + 0*x6 + 0*x7 + 0*x8 + 0*x9 + 0*x10 + 0*x11 + 0*x12 + 0*x13 + 0*x14 + 0*x15 + 0*x16);
			(r1, r2, r3, r4) = q2.ToArrays();
			TestHelper.AssertEqual(expect, r1);
			TestHelper.AssertEqual(expect, r2);
			TestHelper.AssertEqual(expect, r3);
			TestHelper.AssertEqual(expect, r4);

		}

		#endregion

	}
}
