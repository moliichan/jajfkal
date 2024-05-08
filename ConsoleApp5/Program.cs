using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ВВЕДИТЕ КОЛ-ВО УРАВНЕНИЙ В СИСТЕМЕ:");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("\nВВЕДИТЕ КОЛ-ВО НЕИЗВЕСТНЫХ:");
            int m = int.Parse(Console.ReadLine());

            SystemOfLinearEquation system = new SystemOfLinearEquation(n, m + 1);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < m + 1; j++)
                {
                    system[i][j] = double.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("\n" + system);

            Console.WriteLine("\nСТУПЕНЧАТЫЙ ВИД СИСТЕМЫ:\n");
            system.ToEchelonForm();
            Console.WriteLine(system + "\n");

            var solution = system.Gauss();
            if (solution != null)
            {
                for (int i = 0; i < solution.Length; i++)
                {
                    Console.WriteLine("x" + (i + 1) + " = " + solution[i]);
                }
            }

            Console.Read();
        }
    }
}