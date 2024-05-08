using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp5;

namespace Class2Test
{
    [TestClass]
    public class SystemOfLinearEquationTest
    {
        // количество переменных
        [TestMethod]
        public void Constructor_SetsNumberOfVariables()
        {
            int numVariables = 3;
            SystemOfLinearEquation system = new SystemOfLinearEquation(numVariables);
            Assert.AreEqual(numVariables, system.NumberOfVariables);
        }

        // обращение к уравнению по его номеру в СЛУ
        [TestMethod]
        public void Indexer_GetsAndSetsEquation()
        {
            int numVariables = 3;
            SystemOfLinearEquation system = new SystemOfLinearEquation(numVariables);
            int equationNumber = 1;

            LinearEquation equation = system[equationNumber];
            equation[2] = 5.0;

            Assert.AreEqual(5.0, equation[2]);
        }

        // приведение к ступенчатому виду
        [TestMethod]
        public void ToReduceEchelonForm_ReducesSystem()
        {
            SystemOfLinearEquation system = new SystemOfLinearEquation(3);
            system[0][0] = 1; system[0][1] = 2; system[0][2] = 3; system[0][3] = 4;
            system[1][0] = 5; system[1][1] = 6; system[1][2] = 7; system[1][3] = 8;
            system[2][0] = 9; system[2][1] = 10; system[2][2] = 11; system[2][3] = 12;

            system.ReduceEchelonForm();

            Assert.AreEqual(0, system[0][0]);
            Assert.AreEqual(1, system[0][1]);
            Assert.AreEqual(0, system[0][2]);
            Assert.AreEqual(0, system[0][3]);

            Assert.AreEqual(0, system[1][0]);
            Assert.AreEqual(0, system[1][1]);
            Assert.AreEqual(1, system[1][2]);
            Assert.AreEqual(0, system[1][3]);

            Assert.AreEqual(0, system[2][0]);
            Assert.AreEqual(0, system[2][1]);
            Assert.AreEqual(0, system[2][2]);
            Assert.AreEqual(1, system[2][3]);
        }

        // решение СЛУ
        [TestMethod]
        public void Solve_ReturnsSolution()
        {
            SystemOfLinearEquation system = new SystemOfLinearEquation(3);
            system[0][0] = 1; system[0][1] = 2; system[0][2] = 3; system[0][3] = 4;
            system[1][0] = 5; system[1][1] = 6; system[1][2] = 7; system[1][3] = 8;
            system[2][0] = 9; system[2][1] = 10; system[2][2] = 11; system[2][3] = 12;

            double[] solution = system.Solve();

            Assert.AreEqual(2, solution[0]);
            Assert.AreEqual(3, solution[1]);
            Assert.AreEqual(4, solution[2]);
        }

        [TestMethod]
        public void Solve_ThrowsExceptionForInconsistentSystem()
        {
            SystemOfLinearEquation system = new SystemOfLinearEquation(3);
            system[0][0] = 1; system[0][1] = 2; system[0][2] = 3; system[0][3] = 4;
            system[1][0] = 5; system[1][1] = 6; system[1][2] = 7; system[1][3] = 8;
            system[2][0] = 9; system[2][1] = 10; system[2][2] = 11; system[2][3] = 13;

            Assert.ThrowsException<InvalidOperationException>(() => { system.Solve(); });
        }
    }
}