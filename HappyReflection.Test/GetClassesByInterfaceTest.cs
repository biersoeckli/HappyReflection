using HappyReflection.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyReflection.Test
{
    public class GetClassesByInterfaceTest
    {
        [Test]
        public void TestValidByGenericType()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByInterface<IChocolate>();

            Assert.AreEqual(2, foundTypes.Count());
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithAttribute)));
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithInterface)));
        }

        [Test]
        public void TestValidByTypeVariable()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByInterface(typeof(IChocolate));

            Assert.AreEqual(2, foundTypes.Count());
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithAttribute)));
            Assert.IsTrue(foundTypes.Contains(typeof(ChocolateWithInterface)));
        }

        [Test]
        public void TestNoResultsFound()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByInterface<IUnusedInterface>();
            Assert.IsEmpty(foundTypes);
        }

        [Test]
        public void TestInputNull()
        {
            Assert.Throws<ArgumentNullException>(() => HappyReflection.GetClassesByInterface(null));
        }
    }
}