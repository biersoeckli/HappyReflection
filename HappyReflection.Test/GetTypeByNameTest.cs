using HappyReflection.Test.Models;
using NUnit.Framework;
using System;

namespace HappyReflection.Test
{
    public class GetTypeByNameTest
    {
        [Test]
        public void TestValidByGenericType()
        {
            Assert.AreEqual(typeof(ChocolateWithInterface), HappyReflection.GetTypeByName("chocolatewithinterface"));
            Assert.AreEqual(typeof(ChocolateWithAttribute), HappyReflection.GetTypeByName("ChocolateWithAttribute"));
        }

        [Test]
        public void TestInputNull()
        {
            Assert.Throws<ArgumentNullException>(() => HappyReflection.GetTypeByName(null));
        }
    }
}