using HappyReflection.Models;
using HappyReflection.Test.Attributes;
using HappyReflection.Test.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyReflection.Test
{
    public class GetClassesWithInterfacesByAttributeTest
    {
        [Test]
        public void TestValidByGenericType()
        {
            IList<HappyReflectionTypeWithInterface> foundTypeWithInterfaces = HappyReflection.GetClassesWithInterfacesByAttribute<NomNomNomAttribute>();

            Assert.AreEqual(typeof(IChocolate), foundTypeWithInterfaces.First().Interfaces.First());
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypeWithInterfaces.First().Type);
        }

        [Test]
        public void TestValidByTypeVariable()
        {
            IList<HappyReflectionTypeWithInterface> foundTypeWithInterfaces = HappyReflection.GetClassesWithInterfacesByAttribute(typeof(NomNomNomAttribute));

            Assert.AreEqual(typeof(IChocolate), foundTypeWithInterfaces.First().Interfaces.First());
            Assert.AreEqual(typeof(ChocolateWithAttribute), foundTypeWithInterfaces.First().Type);
        }

        [Test]
        public void TestNoResultsFound()
        {
            IList<HappyReflectionTypeWithInterface>? foundTypeWithInterfaces = HappyReflection.GetClassesWithInterfacesByAttribute<NotAnnotatedAttribute>();
            Assert.IsEmpty(foundTypeWithInterfaces);
        }

        [Test]
        public void TestInputNull()
        {
            Assert.Throws<ArgumentNullException>(() => HappyReflection.GetClassesWithInterfacesByAttribute(null));
        }
    }
}