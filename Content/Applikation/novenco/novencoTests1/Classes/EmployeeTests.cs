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
    public class EmployeeTests
    {
        [TestMethod()]
        public void GetPathNameTest_SetDisplayMemberPath_ReturnsStringName()
        {
            // Arrange
            Employee employee = new Employee();
            bool result = false;

            // Act
            string temp = employee.GetPathName();
            if (temp == "Name")
            {
                result = true;
            }

            // Assert
            Assert.IsTrue(result);
        }
    }
}


