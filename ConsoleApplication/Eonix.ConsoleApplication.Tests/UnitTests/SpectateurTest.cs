using Eonix.ConsoleApplication.Builders;
using Eonix.ConsoleApplication.Model;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Eonix.ConsoleApplication.Tests.UnitTests;

[TestClass]
public class SpectateurTest
{
    [TestMethod]
    public void UnSpectateurDoitApplaudirAUneRepresentationArtistique()
    {
        // Given
        var reactionTourMusical = A.Fake<Action<Spectateur, PerformanceEvent>>();
        var reactionTourAcrobatique = A.Fake<Action<Spectateur, PerformanceEvent>>();
        var singe = SingeBuilder.Nouveau("Test singe").ApprendreTourAcrobatique("Danser").Creer();
        var spectateur = SpectateurBuilder.Nouveau("Test spectateur")
            .ReactionTourMusical(reactionTourMusical)
            .ReactionTourAcrobatique(reactionTourAcrobatique)
            .Creer();
        spectateur.Observer(singe);

        // When
        singe.Executer("Danser");

        // Then
        A.CallTo(() => reactionTourAcrobatique(A<Spectateur>._, A<PerformanceEvent>._)).MustHaveHappened();
        A.CallTo(() => reactionTourMusical(A<Spectateur>._, A<PerformanceEvent>._)).MustNotHaveHappened();
    }

    [TestMethod]
    public void UnSpectateurDoitEffectuerSaReactionAUneRepresentationMusicale()
    {
        // Given
        var reactionTourMusical = A.Fake<Action<Spectateur, PerformanceEvent>>();
        var reactionTourAcrobatique = A.Fake<Action<Spectateur, PerformanceEvent>>();
        var singe = SingeBuilder.Nouveau("Test singe").ApprendreTourMusical("Chanter").Creer();
        var spectateur = SpectateurBuilder.Nouveau("Test spectateur")
            .ReactionTourMusical(reactionTourMusical)
            .ReactionTourAcrobatique(reactionTourAcrobatique)
            .Creer();
        spectateur.Observer(singe);

        // When
        singe.Executer("Chanter");

        // Then
        A.CallTo(() => reactionTourMusical(A<Spectateur>._, A<PerformanceEvent>._)).MustHaveHappened();
        A.CallTo(() => reactionTourAcrobatique(A<Spectateur>._, A<PerformanceEvent>._)).MustNotHaveHappened();
    }
}
