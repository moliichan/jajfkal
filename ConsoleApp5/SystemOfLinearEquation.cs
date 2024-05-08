using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp5;

namespace ConsoleApp5
{
    public class SystemOfLinearEquation
    {
        // список уравнений в СЛУ
        private readonly List<LinearEquation> equations;

        // количество уравнений в СЛУ
        public int NumberOfEquations => equations.Count;

        // количество коэффициентов в СЛУ
        public int NumberOfVariables => equations.Count > 0 ? equations[0].Coefficients.Length : 0;

        public SystemOfLinearEquation(int numEquations, int numVariables)
        {
            equations = new List<LinearEquation>();
            for (int i = 0; i < numEquations; i++)
            {
                equations.Add(new LinearEquation(numVariables));
            }
        }

        // ступенчатый вид
        public void ToEchelonForm()
        {
            if (NumberOfVariables - 1 > NumberOfEquations)
            {
                // количество переменных > количества строк  => система не имеет решений или имеет бесконечно много
            }

            else
            {
                // сравниваем две соседние строки и переставляем строку с нулями в начале ниже
                for (int i = 0; i < NumberOfEquations - 1; i++)
                {
                    // последнюю строку нет смысла менять, в ней либо останется одна неизвестная, либо не будет единственного решения

                    if (FirstNonZeroElement(i) > FirstNonZeroElement(i + 1))
                    {
                        Swap(i, i + 1);
                    }

                    // нашли текущую строку и номер ее ведущего (первого ненулевого) элемента

                    // преобразуем строку
                    if (FirstNonZeroElement(i) != -1)
                    {
                        TransformString(i, FirstNonZeroElement(i));
                    }

                    // в строке нет ненулевых элементов 
                    // и ранг < кол-ва неизвестных
                    else
                    {
                        if (Rang() < NumberOfVariables - 1)
                        {
                            // количество переменных > количества строк  => система не имеет решений или имеет бесконечно много
                            return;
                        }
                    }

                    // преобразуем все строки ниже текущей
                    for (int k = i + 1; k < NumberOfEquations; k++)
                    {
                        TransformBottomString(i, k);
                    }

                    // повторяем для остальных строк
                }
            }
        }

        // первый ненулевой элемент в строке
        public int FirstNonZeroElement(int i)
        {
            for (int j = 0; j < NumberOfVariables - 1; j++)
            {
                if (equations[i][j] != 0.0)
                {
                    // возвращаем номер столбца первого ненулевого элемента
                    return j;
                }
            }
            return -1;
        }

        // перестановка строк
        public void Swap(int str1, int str2)
        {
            for (int j = 0; j < NumberOfVariables; j++)
            {
                double c = equations[str1][j];
                equations[str1][j] = equations[str2][j];
                equations[str2][j] = c;
            }
        }

        // преобразование текущей строки
        public void TransformString(int i, int j)
        {
            double c = equations[i][j]; // ведущий элемент строки

            // если ведущий элемент меньше нуля - умножаем всю строку на -1
            if (equations[i][j] < 0) c = c * (-1);

            // делим все элементы на ведущий, чтобы ведущий стал единичным
            for (int l = 0; l < NumberOfVariables; l++)
            {
                equations[i][l] = equations[i][l] / c;
            }
        }

        // преобразование строки ниже текущей
        public void TransformBottomString(int i, int k)
        {
            if (equations[i][FirstNonZeroElement(k)] != 0)
            {
                // число, на которое будем умножать строку с ведущим элементом и вычитать из текущей, чтобы обнулить
                double c = equations[k][FirstNonZeroElement(k)] / equations[i][FirstNonZeroElement(k)];

                for (int l = 0; l < NumberOfVariables; l++)
                {
                    equations[k][l] = equations[k][l] - c * equations[i][l];
                }
            }
        }
        
        // решение методом Гаусса
        public double[] Gauss()
        {
            // проверка на разрешимость системы и ранг < кол-ва неизвестных
            for (int i = 0; i < NumberOfEquations; i++)
            {
                if (!equations[i])
                {
                    if (Rang() < NumberOfVariables - 1)
                    {
                        Console.WriteLine("\nСЛУ не имеет единственного решения");
                        return null;
                    }
                }
            }

            // массив значений неизвестных
            var solution = new double[NumberOfVariables - 1];

            // начинаем с последней строки
            for (int i = NumberOfEquations - 1; i >= 0; i--)
            {
                // первый ненулевой элемент строки - единственный

                // делим коэффициент B на единственный коэффициент A
                solution[FirstNonZeroElement(i)] = equations[i][NumberOfVariables - 1] / equations[i][FirstNonZeroElement(i)];

                // вычитаем из строк выше найденную неизвестную, умноженную на ее коэффициент
                for (int k = i - 1; k >= 0; k--)
                {
                    equations[k][NumberOfVariables - 1] = equations[k][NumberOfVariables - 1] - equations[k][FirstNonZeroElement(i)] * solution[FirstNonZeroElement(i)];
                    equations[k][FirstNonZeroElement(i)] = 0;
                }

                // повторяем для остальных строк
            }

            return solution;
        }

        // ранг матрицы
        public int Rang()
        {
            int count = 0;

            for (int i = 0; i < NumberOfEquations; i++)
            {
                if (equations[i])
                {
                    count++;
                }
            }

            return count;
        }

        // доступ к уравнениям
        public LinearEquation this[int i]
        {
            get => equations[i];
            set => equations[i] = value;
        }

        // преобразование в строку
        public override string ToString()
        {
            return string.Join("\n", equations.Select(x => x.ToString()));
        }
    }
}