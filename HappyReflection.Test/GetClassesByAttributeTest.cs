using HappyReflection.Test.Attributes;
using HappyReflection.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyReflection.Test
{
    public class GetClassesByAttributeTest
    {
        [Test]
        public void TestValidByGenericType()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByAttribute<NomNomNomAttribute>();
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypes.First());
            Assert.AreEqual(1, foundTypes.Count());
        }

        [Test]
        public void TestValidByTypeVariable()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByAttribute(typeof(NomNomNomAttribute));
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypes.First());
            Assert.AreEqual(1, foundTypes.Count());
        }

        [Test]
        public void TestNoResultsFound()
        {
            IList<Type> foundTypes = HappyReflection.GetClassesByAttribute<NotAnnotatedAttribute>();
            Assert.IsEmpty(foundTypes);
        }

        [Test]
        public void TestInputNull()
        {
            Assert.Throws<ArgumentNullException>(() => HappyReflection.GetClassesByAttribute(null));
        }
    }
}