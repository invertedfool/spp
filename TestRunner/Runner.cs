using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestLibrary;

namespace TestRunner
{
    public class Runner
    {
        private int _p, _f, _s;

        public void RunTests(Assembly asm)
        {
            Console.WriteLine("=== Запуск тестов ===\n");
            
            foreach (var cls in asm.GetTypes().Where(t => t.GetCustomAttribute<TestClassAttribute>() != null))
            {
                Console.WriteLine($"Класс: {cls.Name}");
                var inst = Activator.CreateInstance(cls);
                
                cls.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.GetCustomAttribute<ClassSetupAttribute>() != null)?.Invoke(null, null);

                foreach (var m in cls.GetMethods().Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null || 
                                   m.GetCustomAttribute<AsyncTestMethodAttribute>() != null)
                    .OrderBy(m => m.GetCustomAttribute<TestMethodAttribute>()?.Priority ?? 
                                 m.GetCustomAttribute<AsyncTestMethodAttribute>()?.Priority ?? 100))
                {
                    if (m.GetCustomAttribute<AsyncTestMethodAttribute>() != null) RunAsync(inst, m);
                    else Run(inst, m);
                }

                cls.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.GetCustomAttribute<ClassTeardownAttribute>() != null)?.Invoke(null, null);
                Console.WriteLine();
            }

            Console.WriteLine("=== Итоги ===");
            Console.WriteLine($"Прошло: {_p}\nНе прошло: {_f}\nПропущено: {_s}");
        }

        void Run(object i, MethodInfo m)
        {
            var a = m.GetCustomAttribute<TestMethodAttribute>();
            if (a.Skip) { Console.WriteLine($"  {m.Name} - ПРОПУЩЕН ({a.SkipReason})"); _s++; return; }
            Console.Write($"  {m.Name}... ");
            try
            {
                Setup(i);
                var d = m.GetCustomAttributes<TestDataAttribute>();
                if (d.Any()) foreach (var x in d) m.Invoke(i, x.Parameters);
                else m.Invoke(i, null);
                Teardown(i);
                Console.WriteLine("ПРОШЕЛ");
                _p++;
            }
            catch (Exception ex)
            {
                Teardown(i);
                Console.WriteLine($"НЕ ПРОШЕЛ\n    {ex.InnerException?.Message ?? ex.Message}");
                _f++;
            }
        }

        void RunAsync(object i, MethodInfo m)
        {
            var a = m.GetCustomAttribute<AsyncTestMethodAttribute>();
            if (a.Skip) { Console.WriteLine($"  {m.Name} - ПРОПУЩЕН ({a.SkipReason})"); _s++; return; }
            Console.Write($"  {m.Name} [ASYNC]... ");
            try
            {
                Setup(i);
                var t = (Task)m.Invoke(i, null);
                if (!t.Wait(a.TimeoutMs)) throw new TestLibrary.TimeoutException(a.TimeoutMs);
                Teardown(i);
                Console.WriteLine("ПРОШЕЛ");
                _p++;
            }
            catch (Exception ex)
            {
                Teardown(i);
                Console.WriteLine($"НЕ ПРОШЕЛ\n    {ex.InnerException?.Message ?? ex.Message}");
                _f++;
            }
        }

        void Setup(object i) => i.GetType().GetMethods().FirstOrDefault(m => m.GetCustomAttribute<SetupAttribute>() != null)?.Invoke(i, null);
        void Teardown(object i) { try { i.GetType().GetMethods().FirstOrDefault(m => m.GetCustomAttribute<TeardownAttribute>() != null)?.Invoke(i, null); } catch { } }
    }
}
