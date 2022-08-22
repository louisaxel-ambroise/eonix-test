using Eonix.Api.Model;
using Eonix.Api.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Eonis.Api.Tests;

[TestClass]
public class PersonneFilterTests
{
    [TestMethod]
    [DataRow("Alb", "", 1)]
    [DataRow("us", "", 2)]
    [DataRow("", "M", 2)]
    [DataRow("us", "H", 1)]
    public void AppliquerUnPersonneFilterDoitFiltrerLaCollection(string prenom, string nom, int expectedCount)
    {
        // Given
        var filter = new PersonneFilter { Prenom = prenom, Nom = nom };
        var query = new Personne[]
        {
            new Personne ("Albus", "Dumbledore"),
            new Personne ("Minerva", "McGonagall"),
            new Personne ("Rubeus", "Hagrid"),
            new Personne ("Drago", "Malefoy")
        }.AsQueryable();

        // When
        var result = filter.ApplyTo(query);

        // Then
        Assert.AreEqual(expectedCount, result.Count());
    }
}