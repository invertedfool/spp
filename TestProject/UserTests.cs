using TestLibrary;
using TestedProject;

namespace TestProject
{
    [TestClass]
    public class UserTests
    {
        private User _user;

        [Setup]
        public void Setup() => _user = new User { Name = "Test", Email = "test@mail.com", Age = 25 };

        [TestMethod]
        public void Test_IsAdult_True() => Assert.IsTrue(_user.IsAdult());

        [TestMethod]
        public void Test_IsAdult_False()
        {
            _user.Age = 15;
            Assert.IsFalse(_user.IsAdult());
        }

        [TestMethod]
        public void Test_ValidEmail() => Assert.IsTrue(_user.IsValidEmail());

        [TestMethod]
        public void Test_InvalidAge() => Assert.Throws<System.ArgumentException>(() => _user.Age = -1);

        [TestMethod]
        public void Test_IsNotNull() => Assert.IsNotNull(_user);
    }
}
