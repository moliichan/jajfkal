using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp5;
using System.Threading;

namespace ConsoleApp5
{
    public class LinearEquation
    {
        // создание лин уравнения из массива коэффициентов
        public double[] Coefficients { get; private set; }

        // из строки коэффициентов
        public LinearEquation(string coefficientsString)
        {
            Coefficients = coefficientsString.Split(' ').Select(double.Parse).ToArray();
        }

        // из списка коэффициентов
        public LinearEquation(IEnumerable<double> coefficients)
        {
            Coefficients = coefficients.ToArray();
        }

        // заполнение уравнения нулями
        public LinearEquation(int count)
        {
            Coefficients = Enumerable.Repeat(0.0, count).ToArray();
        }

        // случайными числами
        public void InitializeRandomly()
        {
            var random = new Random();
            for (int i = 0; i < Coefficients.Length; i++)
            {
                Thread.Sleep(20);
                Coefficients[i] = random.NextDouble() * 10 - 5;
            }
        }

        // одинаковыми значениями
        public void InitializeWithSameValue(double value)
        {
            for (int i = 0; i < Coefficients.Length; i++)
            {
                Coefficients[i] = value;
            }
        }

        // сложение уравнений
        public static LinearEquation operator +(LinearEquation a, LinearEquation b)
        {
            if (a.Coefficients.Length != b.Coefficients.Length)
            {
                throw new ArgumentException("Linear equations must have the same number of coefficients");
            }

            var result = new LinearEquation(a.Coefficients.Length);
            for (int i = 0; i < a.Coefficients.Length; i++)
            {
                result[i] = a.Coefficients[i] + b.Coefficients[i];
            }
            return result;
        }

        // вычитание уравнений
        public static LinearEquation operator -(LinearEquation a, LinearEquation b)
        {
            if (a.Coefficients.Length != b.Coefficients.Length)
            {
                throw new ArgumentException("Linear equations must have the same number of coefficients");
            }

            var result = new LinearEquation(a.Coefficients.Length);
            for (int i = 0; i < a.Coefficients.Length; i++)
            {
                result[i] = a.Coefficients[i] - b.Coefficients[i];
            }
            return result;
        }

        // умножение справа
        public static LinearEquation operator *(LinearEquation a, double b)
        {
            var result = new LinearEquation(a.Coefficients.Length);
            for (int i = 0; i < a.Coefficients.Length; i++)
            {
                result[i] = a.Coefficients[i] * b;
            }
            return result;
        }

        // умножение слева
        public static LinearEquation operator *(double a, LinearEquation b) => b * a;

        // умножение на -1
        public static LinearEquation operator -(LinearEquation a) => a * -1;

        // равенство уравнений
        public static bool operator ==(LinearEquation a, LinearEquation b)
        {
            if (a is null || b is null)
            {
                return false;
            }

            return a.Coefficients.SequenceEqual(b.Coefficients);
        }

        // неравенство уравнений
        public static bool operator !=(LinearEquation a, LinearEquation b) => !a.Coefficients.SequenceEqual(b.Coefficients);

        // переопределение метода Equals()
        public override bool Equals(object obj)
        {
            return obj is LinearEquation equation &&
                   Coefficients.SequenceEqual(equation.Coefficients);
        }

        // переопределение метода GetHashCode()
        public override int GetHashCode()
        {
            return Coefficients.GetHashCode();
        }

        // проверка разрешимости системы
        public static implicit operator bool(LinearEquation equation)
        {
            // если все коэффициенты A равны нулю и коэффициент B не равен нулю
            if (equation[equation.Coefficients.Length - 1] != 0)
            {
                for (int i = 0; i < equation.Coefficients.Length - 1; i++)
                {
                    if (equation[i] != 0)
                    {
                        return true;
                    }
                }
            }

            // если все коэффициенты A равны нулю и коэффициент B равен нулю
            else if (equation[equation.Coefficients.Length - 1] == 0)
            {
                for (int i = 0; i < equation.Coefficients.Length - 1; i++)
                {
                    if (equation[i] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // преобразование в список
        public static explicit operator List<double>(LinearEquation equation) => equation.Coefficients.ToList();

        // преобразование в строку
        public override string ToString() => string.Join(" ", Coefficients);

        // доступ к элементам
        public double this[int index]
        {
            get => Coefficients[index];
            set => Coefficients[index] = value;
        }
    }
}