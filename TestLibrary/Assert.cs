using System;
using System.Collections.Generic;
using System.Linq;

namespace TestLibrary
{
    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
                throw new EqualException(expected, actual);
        }

        public static void AreNotEqual<T>(T notExpected, T actual)
        {
            if (EqualityComparer<T>.Default.Equals(notExpected, actual))
                throw new EqualException(notExpected, actual);
        }

        public static void IsNull(object obj)
        {
            if (obj != null) throw new NullException("Должен быть null");
        }

        public static void IsNotNull(object obj)
        {
            if (obj == null) throw new NullException("Не должен быть null");
        }

        public static void IsTrue(bool condition)
        {
            if (!condition) throw new AssertException("Должно быть true");
        }

        public static void IsFalse(bool condition)
        {
            if (condition) throw new AssertException("Должно быть false");
        }

        public static void IsInstanceOfType<T>(object obj)
        {
            if (obj == null || !(obj is T))
                throw new TypeException(typeof(T), obj?.GetType());
        }

 
        public static void Contains<T>(IEnumerable<T> collection, T item)
        {
            if (!collection.Contains(item))
                throw new CollectionException($"Не содержит: {item}");
        }

        public static void DoesNotContain<T>(IEnumerable<T> collection, T item)
        {
            if (collection.Contains(item))
                throw new CollectionException($"Содержит: {item}");
        }

        public static void Throws<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
                throw new AssertException($"Ожидалось {typeof(TException).Name}");
            }
            catch (TException) { }
        }
    }
}
