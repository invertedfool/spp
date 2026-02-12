using System;
using System.Reflection;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var assembly = Assembly.Load("TestProject");
                var runner = new Runner();
                runner.RunTests(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
