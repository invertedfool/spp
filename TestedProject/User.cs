using System;

namespace TestedProject
{
    public class User
    {
        private int _age;
        
        public string Name { get; set; }
        public string Email { get; set; }
        
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 150)
                    throw new ArgumentException("Возраст от 0 до 150");
                _age = value;
            }
        }

        public bool IsAdult() => Age >= 18;
        
        public bool IsValidEmail() => Email.Contains("@") && Email.Contains(".");
    }
}
