
using System;

namespace FilterTests
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SameResultAttribute : Attribute
    {
    }
}