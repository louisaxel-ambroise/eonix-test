using Eonix.ConsoleApplication.Model;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Eonix.ConsoleApplication.Tests.UnitTests;
[TestClass]
public class SingeTest
{
    [TestMethod]
    public void UnSingeNePeutPasExecuterUnTourInconnu()
    {
        // Given
        var tours = new List<Tour>();
        var singe = new Singe("Test singe", tours);
        var reaction = A.Fake<EventHandler<PerformanceEvent>>();

        singe.Reactions += reaction;

        // When
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => singe.Executer("tourInconnu"));
        A.CallTo(() => reaction.Invoke(singe, A<PerformanceEvent>._)).MustNotHaveHappened();
    }

    [TestMethod]
    public void UnSingePeutExecuterUnTourConnu()
    {
        // Given
        var tours = new List<Tour> { new Tour("Jongler", TypeDeTour.Acrobatique) };
        var singe = new Singe("Test singe", tours);
        var reaction = A.Fake<EventHandler<PerformanceEvent>>();

        singe.Reactions += reaction;

        // When
        singe.Executer("Jongler");

        // Then
        A.CallTo(() => reaction.Invoke(singe, A<PerformanceEvent>._)).MustHaveHappened();
    }
}
