using HappyReflection.Models;
using HappyReflection.Test.Attributes;
using HappyReflection.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyReflection.Test
{
    [TestClass]
    public class GetClassesWithInterfacesByAttributeTest
    {
        [TestMethod]
        public void TestValidByGenericType()
        {
            IList<HappyReflectionTypeWithInterface> foundTypeWithInterfaces = HappyReflection.GetClassesWithInterfacesByAttribute<NomNomNomAttribute>();

            Assert.AreEqual(typeof(IChocolate), foundTypeWithInterfaces.First().Interfaces.First());
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypeWithInterfaces.First().Type);
        }

        [TestMethod]
        public void TestValidByTypeVariable()
        {
            IList<HappyReflectionTypeWithInterface> foundTypeWithInterfaces = HappyReflection.GetClassesWithInterfacesByAttribute(typeof(NomNomNomAttribute));

            Assert.AreEqual(typeof(IChocolate), foundTypeWithInterfaces.First().Interfaces.First());
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypeWithInterfaces.First().Type);
        }

        [TestMethod]
        public void TestNoResultsFound()
        {
            IList<HappyReflectionTypeWithInterface>? foundTypeWithInterfaces = HappyReflection.GetClassesWithInterfacesByAttribute<NotAnnotatedAttribute>();
            Assert.AreEqual(0, foundTypeWithInterfaces.Count);
        }

        [TestMethod]
        public void TestInputNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => HappyReflection.GetClassesWithInterfacesByAttribute(null));
        }
    }
}