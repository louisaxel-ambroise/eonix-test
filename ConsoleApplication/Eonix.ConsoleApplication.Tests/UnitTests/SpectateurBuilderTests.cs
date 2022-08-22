using Eonix.ConsoleApplication.Builders;
using Eonix.ConsoleApplication.Model;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Eonix.ConsoleApplication.Tests.UnitTests;

[TestClass]
public class SpectateurBuilderTests
{
    [TestMethod]
    public void LeSpectateurCreeDoitAvoirLeNomEtLesToursSpecifies()
    {
        // Given
        var reactionTourMusical = A.Fake<Action<Spectateur, PerformanceEvent>>();
        var builder = SpectateurBuilder.Nouveau("Test Spectateur")
            .ReactionTourMusical(reactionTourMusical);

        // When
        var spectateur = builder.Creer();

        // Then
        Assert.AreEqual("Test Spectateur", spectateur.Nom);
        Assert.AreEqual(1, spectateur.Reactions.Count);
        Assert.IsTrue(spectateur.Reactions.TryGetValue(TypeDeTour.Musical, out var _));
        Assert.IsFalse(spectateur.Reactions.TryGetValue(TypeDeTour.Acrobatique, out var _));
    }
    [TestMethod]
    public void CreerUnSingeSansReactionsDoitLeverUneException()
    {
        // Given
        var reactionTourMusical = A.Fake<Action<Spectateur, PerformanceEvent>>();
        var builder = SpectateurBuilder.Nouveau("Test Spectateur");

        // When
        Assert.ThrowsException<InvalidOperationException>(() => builder.Creer());
    }
}
