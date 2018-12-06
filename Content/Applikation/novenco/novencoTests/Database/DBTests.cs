using Microsoft.VisualStudio.TestTools.UnitTesting;
using novenco.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Database.Tests
{
    [TestClass()]
    public class DBTests
    {
        [TestMethod]
        public void StoreTestConnectionTest_ConnectionIsMade_ReturnsTrue()
        {
            // Arrange
            // Database er static og tilgængelig
            bool result;
            
            // Act
            result = DB.StoreTestConnection();

            // Assert
            Assert.IsTrue(result);
        }
    }
}