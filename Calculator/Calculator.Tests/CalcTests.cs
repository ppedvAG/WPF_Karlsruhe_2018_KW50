using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_5_and_6_Results()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(5, 6);

            //Assert
            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_Results()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calc_Sum_MAX_and_1_Results()
        {
            //Arrange
            var calc = new Calc();

            //Act
            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }
    }
}
