using Eonix.ConsoleApplication.Builders;
using Eonix.ConsoleApplication.Model;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Eonix.ConsoleApplication.Tests.UnitTests;

[TestClass]
public class DresseurTests
{

    [TestMethod]
    public void UnDresseurPeutFaireRealiserTousLesToursDeSonSinge()
    {
        // Given
        var singe = SingeBuilder.Nouveau("Test singe").ApprendreTourMusical("Tour 1").ApprendreTourAcrobatique("Tour 2").Creer();
        var dresseur = new Dresseur("Dresseur test", singe);
        var reaction = A.Fake<EventHandler<PerformanceEvent>>();

        singe.Reactions += reaction;

        // When
        dresseur.Jouer();

        // Then
        A.CallTo(() => reaction(A<object>._, A<PerformanceEvent>._)).MustHaveHappenedTwiceExactly();
        A.CallTo(() => reaction(singe, A<PerformanceEvent>.That.Matches(x => x.Tour == "Tour 1" && x.Type == TypeDeTour.Musical && x.Artiste == "Test singe"))).MustHaveHappened();
        A.CallTo(() => reaction(singe, A<PerformanceEvent>.That.Matches(x => x.Tour == "Tour 2" && x.Type == TypeDeTour.Acrobatique && x.Artiste == "Test singe"))).MustHaveHappened();
    }
}
