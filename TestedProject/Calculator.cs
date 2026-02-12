using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestedProject
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        
        public double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException();
            return a / b;
        }
        
        public long Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("n >= 0");
            long r = 1;
            for (int i = 2; i <= n; i++) r *= i;
            return r;
        }
        
        public bool IsPrime(int n)
        {
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            for (int i = 3; i <= Math.Sqrt(n); i += 2)
                if (n % i == 0) return false;
            return true;
        }
        
        public double Average(int[] nums)
        {
            if (nums == null || nums.Length == 0) throw new ArgumentException("Пусто");
            return nums.Average();
        }
        
        public async Task<int> AddAsync(int a, int b)
        {
            await Task.Delay(50);
            return a + b;
        }
    }
}
