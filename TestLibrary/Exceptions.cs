using System;

namespace TestLibrary
{
    public class AssertException : Exception
    {
        public AssertException(string msg) : base(msg) { }
    }

    public class EqualException : AssertException
    {
        public EqualException(object exp, object act) : base($"Ожидалось: {exp}, получено: {act}") { }
    }

    public class NullException : AssertException
    {
        public NullException(string msg) : base(msg) { }
    }

    public class TypeException : AssertException
    {
        public TypeException(Type exp, Type act) : base($"Тип: {exp.Name} != {act.Name}") { }
    }

    public class CollectionException : AssertException
    {
        public CollectionException(string msg) : base(msg) { }
    }

    public class TimeoutException : AssertException
    {
        public TimeoutException(int ms) : base($"Таймаут: {ms}ms") { }
    }
}
