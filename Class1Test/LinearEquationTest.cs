using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp5;
using System.Collections.Generic;
using System.Linq;

namespace Class1Test
{
    [TestClass]
    public class LinearEquationTest
    {
        // создание уравнения из массива коэффициентов
        [TestMethod]
        public void Constructor_FromString_CreatesEquation()
        {
            var equation = new LinearEquation("1 2 3");
            Assert.AreEqual(new double[] { 1, 2, 3 }, equation.Coefficients);
        }

        // создание уравнения из списка коэффициентов
        [TestMethod]
        public void Constructor_FromList_CreatesEquation()
        {
            var coefficients = new List<double> { 1, 2, 3 };
            var equation = new LinearEquation(coefficients);
            Assert.AreEqual(coefficients, equation.Coefficients);
        }

        // создание уравнения с нулевыми коэффициентами
        [TestMethod]
        public void Constructor_FromZeroes_CreatesZeroEquation()
        {
            var equation = new LinearEquation(3);
            Assert.AreEqual(new double[] { 0, 0, 0 }, equation.Coefficients);
        }

        // задание коэффициентов случайными числами
        [TestMethod]
        public void Initialize_Random_SetsRandomCoefficients()
        {
            var equation = new LinearEquation(3);
            equation.InitializeRandomly();
            Assert.IsTrue(equation.Coefficients.All(x => x != 0));
        }

        // задание коэффициентов одинаковыми значениями
        [TestMethod]
        public void Initialize_SameValue_SetsSameCoefficients()
        {
            var equation = new LinearEquation(3);
            equation.InitializeWithSameValue(42);
            Assert.IsTrue(equation.Coefficients.All(x => x == 42));
        }

        // сложение уравнений
        [TestMethod]
        public void Addition_AddsCoefficients()
        {
            var a = new LinearEquation("1 2 3");
            var b = new LinearEquation("4 5 6");
            var result = a + b;
            Assert.AreEqual(new double[] { 5, 7, 9 }, result.Coefficients);
        }

        // вычитание уравнений
        [TestMethod]
        public void Subtraction_SubtractsCoefficients()
        {
            var a = new LinearEquation("1 2 3");
            var b = new LinearEquation("4 5 6");
            var result = a - b;
            Assert.AreEqual(new double[] { -3, -3, -3 }, result.Coefficients);
        }

        // умножение слева
        [TestMethod]
        public void Multiplication_ByNumber_ScalesCoefficients()
        {
            var a = new LinearEquation("1 2 3");
            var result = a * 2;
            Assert.AreEqual(new double[] { 2, 4, 6 }, result.Coefficients);
        }

        // умножение справа
        [TestMethod]
        public void Multiplication_NumberByEquation_ScalesCoefficients()
        {
            var a = new LinearEquation("1 2 3");
            var result = 2 * a;
            Assert.AreEqual(new double[] { 2, 4, 6 }, result.Coefficients);
        }

        // умножение на -1
        [TestMethod]
        public void UnaryMinus_ScalesCoefficientsByMinusOne()
        {
            var a = new LinearEquation("1 2 3");
            var result = -a;
            Assert.AreEqual(new double[] { -1, -2, -3 }, result.Coefficients);
        }

        // равенство уравнений
        [TestMethod]
        public void Equality_ComparesCoefficients()
        {
            var a = new LinearEquation("1 2 3");
            var b = new LinearEquation("1 2 3");
            Assert.IsTrue(a == b);
        }

        // неравенство уравнений
        [TestMethod]
        public void Inequality_ComparesCoefficients()
        {
            var a = new LinearEquation("1 2 3");
            var b = new LinearEquation("1 2 4");
            Assert.IsTrue(a != b);
        }

        // противоречивое уравнение
        [TestMethod]
        public void False_IsContradictory()
        {
            var a = new LinearEquation("0 1 0");
            Assert.IsTrue(!a);
        }

        // разрешимое уравнение
        [TestMethod]
        public void True_IsSolvable()
        {
            var a = new LinearEquation("1 2 3");
            Assert.IsTrue(a);
        }

        // переопределение ToString()
        [TestMethod]
        public void ToString_ReturnsCoefficientsAsString()
        {
            var equation = new LinearEquation("1 2 3");
            Assert.AreEqual("1 2 3", equation.ToString());
        }

        // неявное преобразование к списку
        [TestMethod]
        public void ImplicitOperator_ConvertsToList()
        {
            var equation = new LinearEquation("1 2 3");
            var coefficients = (List<double>)equation;
            Assert.AreEqual(new List<double> { 1, 2, 3 }, coefficients);
        }
    }
}