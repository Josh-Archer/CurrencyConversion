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
    public class DatasetTests
    {
        [TestMethod()]
        public void DatasetTest()
        {
            Dataset ds = new Dataset();

            if (ds.Denominations != null || ds.NumDenominations != 0 || ds.NumPrices != 0 
                || ds.PriceDifference != 0)
            {
                Assert.Fail("One of Datasets values initialized incorrectly");
            }
        }

        [TestMethod()]
        public void DatasetTest1()
        {

            Dataset ds = new Dataset(1, 1);

            if (ds.NumPrices != 1 || ds.NumDenominations != 1)
                Assert.Fail();
        }
    }
}