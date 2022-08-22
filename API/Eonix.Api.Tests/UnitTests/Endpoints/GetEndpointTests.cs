using Eonix.Api.Controllers;
using Eonix.Api.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Eonis.Api.Tests;

[TestClass]
public class GetEndpointTests
{
    [TestMethod]
    [DataRow(ContextFactory.Id2, "Hagrid", "Rubeus")]
    [DataRow(ContextFactory.Id4, "Malefoy", "Drago")]
    public async Task LaReponseDoitContenirLaPersonneCorrespondante(string id, string nom, string prenom)
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);

        // When
        var result = await controller.GetAsync(Guid.Parse(id), default) as OkObjectResult;

        // Then
        Assert.IsInstanceOfType(result.Value, typeof(PersonneResponse));
        var personne = result.Value as PersonneResponse;
        Assert.AreEqual(Guid.Parse(id), personne.Id);
        Assert.AreEqual(nom, personne.Nom);
        Assert.AreEqual(prenom, personne.Prenom);
    }

    [TestMethod]
    public async Task UnIdentifiantInconnuDoitRetournerNotFound()
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);

        // When
        await Assert.ThrowsExceptionAsync<NullReferenceException>(() => controller.GetAsync(Guid.Empty, default));
    }
}
