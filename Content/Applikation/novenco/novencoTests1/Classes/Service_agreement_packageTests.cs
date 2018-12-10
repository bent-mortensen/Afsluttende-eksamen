using Microsoft.VisualStudio.TestTools.UnitTesting;
using novenco.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes.Tests
{
    [TestClass()]
    public class Service_agreement_packageTests
    {
        [TestMethod()]
        public void NewCelciusTest_ValidateIfNumberIsValid_ReturnsTrue()
        {
            // Arrange
            Service_agreement_package sap = new Service_agreement_package();
            sap.Celcius = 60;
            bool result = false;

            // Act
            result = sap.NewCelcius(50);

            // Assert
            Assert.IsTrue(result, "result is " + result.ToString());

        }
        [TestMethod()]
        public void NewCelciusTest_ValidateIfNumberIsValid_ReturnsFalse()
        {
            // Arrange
            Service_agreement_package sap = new Service_agreement_package();
            sap.Celcius = 60;
            bool result = false;

            // Act
            result = sap.NewCelcius(0);

            // Assert
            Assert.IsTrue(result, "result is " + result.ToString());

        }

    }
}