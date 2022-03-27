using HappyReflection.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HappyReflection.Test
{
    [TestClass]
    public class GetTypeByNameTest
    {
        [TestMethod]
        public void TestValidByGenericType()
        {
            Assert.AreEqual(typeof(ChocolateWithInterface), HappyReflection.GetTypeByName("chocolatewithinterface"));
            Assert.AreEqual(typeof(ChocolateWithAttribute), HappyReflection.GetTypeByName("ChocolateWithAttribute"));
        }

        [TestMethod]
        public void TestInputNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => HappyReflection.GetTypeByName(null));
        }
    }
}