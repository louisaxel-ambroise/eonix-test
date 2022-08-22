using Eonix.ConsoleApplication.Builders;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Eonix.ConsoleApplication.Tests.UnitTests
{
    [TestClass]
    public class SingeBuilderTests
    {
        [TestMethod]
        public void LeSingeCreeDoitAvoirLeNomEtLesToursSpecifies()
        {
            // Given
            var builder = SingeBuilder.Nouveau("Test Singe")
                .ApprendreTourMusical("Tour M")
                .ApprendreTourAcrobatique("Tour A");

            // When
            var singe = builder.Creer();

            // Then
            Assert.AreEqual("Test Singe", singe.Nom);
            Assert.AreEqual(2, singe.ToursConnus().Count());
            Assert.IsTrue(singe.ToursConnus().Contains("Tour M"));
            Assert.IsTrue(singe.ToursConnus().Contains("Tour A"));
        }

        [TestMethod]
        public void CreerUnSingeSansToursDoitLeverUneException()
        {
            // Given
            var builder = SingeBuilder.Nouveau("Test Singe");

            // When
            Assert.ThrowsException<InvalidOperationException>(() => builder.Creer());
        }
    }
}
