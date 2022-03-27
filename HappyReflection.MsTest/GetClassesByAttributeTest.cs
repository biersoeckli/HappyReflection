using HappyReflection.Test.Attributes;
using HappyReflection.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyReflection.Test
{
    [TestClass]
    public class GetClassesByAttributeTest
    {
        [TestMethod]
        public void TestValidByGenericType()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByAttribute<NomNomNomAttribute>();
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypes.First());
            Assert.AreEqual(1, foundTypes.Count());
        }

        [TestMethod]
        public void TestValidByTypeVariable()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByAttribute(typeof(NomNomNomAttribute));
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypes.First());
            Assert.AreEqual(1, foundTypes.Count());
        }

        [TestMethod]
        public void TestNoResultsFound()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByAttribute<NotAnnotatedAttribute>();
            Assert.AreEqual(0, foundTypes.Count);
        }

        [TestMethod]
        public void TestInputNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => HappyReflection.GetClassesByAttribute(null));
        }
    }
}