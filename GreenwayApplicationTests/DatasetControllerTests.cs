using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenwayApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenwayApplication.Tests
{
    [TestClass()]
    public class DatasetControllerTests
    {
        [TestMethod()]
        public void DatasetControllerTest()
        {
            DatasetController controller = new DatasetController();

            if (controller == null)
            {
                Assert.Fail("Controller did not initialize");
            }
        }

        [TestMethod()]
        public void CalculatePriceDifferenceTest()
        {
            //Try a correct one
            Dataset ds = new Dataset(2, 2);
            int[] conversionFactors = { 2 };
            int[] itemFactor1 = { 1, 1 };
            int[][] itemFactors = { itemFactor1, itemFactor1 };

            //create the controller and pass everything to it
            DatasetController controller = new DatasetController();

            if (controller.CalculatePriceDifference(ds, conversionFactors, itemFactors) != 1)
            {
                Assert.Fail("Calculated price was expected to be 1");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Test was supposed to not return anything due to malformed convesion line or second line of data set")]
        public void CalculatePriceDifferenceIncorrectConversionTest() {
            //Now check with bad values for conversionFactors
            Dataset ds = new Dataset(2, 2);
            int[] conversionFactors = new[] { 2, 2 };
            int[] itemFactor1 = new[] { 1, 1 };
            int[][] itemFactors = new[] { itemFactor1, itemFactor1 };

            //create the controller and pass everything to it
            DatasetController controller = new DatasetController();

            //if throws error it worked
            controller.CalculatePriceDifference(ds, conversionFactors, itemFactors);
        }
        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Test was supposed to not return anything due to having too many items in one row of the data set")]
        public void CalculatePriceDifferenceIncorrectColumnItemsTest() {
            //Try an incorrect one with wrong column amount of items
            Dataset ds = new Dataset(2, 2);
            int[] conversionFactors = { 2 };
            int[] itemFactor1 = new[] { 1, 1, 1 };
            int[][] itemFactors = new[] { itemFactor1, itemFactor1 };

            //create the controller and pass everything to it
            DatasetController controller = new DatasetController();

            //should throw error
            controller.CalculatePriceDifference(ds, conversionFactors, itemFactors);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Test was supposed to not return anything due to having too many rows of items in the data set")]
        public void CalculatePriceDifferenceIncorrectRowItemsTest()
        {
            //Try an incorrect one with wrong row amount of items
            Dataset ds = new Dataset(2, 2);
            int[] conversionFactors = new[] { 2 };
            int[] itemFactor1 = new[] { 1, 1 };
            int[][] itemFactors = new[] { itemFactor1 };

            //create the controller and pass everything to it
            DatasetController controller = new DatasetController();

            controller.CalculatePriceDifference(ds, conversionFactors, itemFactors);
        }
    }
}