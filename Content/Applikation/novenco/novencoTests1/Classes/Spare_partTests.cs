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
    public class Spare_partTests
    {
        [TestMethod()]
        public void GetPathSparePartNameTest_SetDisplayMemberPath_ReturnStringSpare_Part_Name()
        {
            // Arrange
            Spare_part spare_part = new Spare_part();
            bool result = false;

            // Act
            string temp = spare_part.GetPathSparePartName();
            if (temp == "Spare_part_name")
            {
                result = true;
            }
            
            // Assert
            Assert.IsTrue(result);
        }
    }
}