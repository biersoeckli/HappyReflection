using HappyReflection.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyReflection.Test
{
    [TestClass]
    public class GetClassesByInterfaceTest
    {
        [TestMethod]
        public void TestValidByGenericType()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByInterface<IChocolate>();

            Assert.AreEqual(2, foundTypes.Count());
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithAttribute)));
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithInterface)));
        }

        [TestMethod]
        public void TestValidByTypeVariable()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByInterface(typeof(IChocolate));

            Assert.AreEqual(2, foundTypes.Count());
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithAttribute)));
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithInterface)));
        }

        [TestMethod]
        public void TestNoResultsFound()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByInterface<IUnusedInterface>();
            Assert.AreEqual(0, foundTypes.Count);
        }

        [TestMethod]
        public void TestInputNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => HappyReflection.GetClassesByInterface(null));
        }
    }
}