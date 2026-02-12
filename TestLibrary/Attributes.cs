using System;

namespace TestLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class SetupAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class TeardownAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class ClassSetupAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class ClassTeardownAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestMethodAttribute : Attribute
    {
        public int Priority { get; set; } = 100;
        public bool Skip { get; set; } = false;
        public string SkipReason { get; set; } = "";
        public string Description { get; set; } = "";
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncTestMethodAttribute : Attribute
    {
        public int Priority { get; set; } = 100;
        public bool Skip { get; set; } = false;
        public string SkipReason { get; set; } = "";
        public int TimeoutMs { get; set; } = 10000;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestDataAttribute : Attribute
    {
        public object[] Parameters { get; set; }
        public TestDataAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }
    }
}
