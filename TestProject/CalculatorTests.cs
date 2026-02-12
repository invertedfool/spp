using System.Threading.Tasks;
using TestLibrary;
using TestedProject;

namespace TestProject
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _c;

        [ClassSetup]
        public static void ClassSetup() => System.Console.WriteLine("ClassSetup");
        [Setup]
        public void Setup() => _c = new Calculator();
        [Teardown]
        public void Teardown() => _c = null;
        [ClassTeardown]
        public static void ClassTeardown() => System.Console.WriteLine("ClassTeardown");

        [TestMethod(Priority = 1, Description = "Сложение")]
        public void Test_Add() => Assert.AreEqual(8, _c.Add(5, 3));

        [TestMethod(Priority = 2)]
        public void Test_Subtract() => Assert.AreEqual(2, _c.Subtract(5, 3));

        [TestMethod]
        [TestData(10, 2, 5.0)]
        [TestData(100, 4, 25.0)]
        public void Test_Divide(double a, double b, double exp) => Assert.AreEqual(exp, _c.Divide(a, b));

        [TestMethod]
        public void Test_DivByZero() => Assert.Throws<System.DivideByZeroException>(() => _c.Divide(10, 0));

        [AsyncTestMethod(TimeoutMs = 5000)]
        public async Task Test_AddAsync() => Assert.AreEqual(30, await _c.AddAsync(10, 20));

        [TestMethod(Skip = true, SkipReason = "Не готов")]
        public void Test_Skip() => Assert.IsTrue(false);

        [TestMethod]
        public void Test_Factorial() => Assert.AreEqual(120, _c.Factorial(5));

        [TestMethod]
        public void Test_IsPrime()
        {
            Assert.IsTrue(_c.IsPrime(7));
            Assert.IsFalse(_c.IsPrime(4));
        }

        [TestMethod]
        public void Test_Average() => Assert.AreEqual(3.0, _c.Average(new[] { 1, 2, 3, 4, 5 }));
    }
}
