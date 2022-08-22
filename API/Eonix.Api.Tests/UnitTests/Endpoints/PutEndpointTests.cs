using Eonix.Api.Controllers;
using Eonix.Api.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eonis.Api.Tests;

[TestClass]
public class PutEndpointTests
{
    [TestMethod]
    public async Task EditerUnePersonneDoitRetournerUneReponse204NoContent()
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);
        var id = Guid.Parse(ContextFactory.Id1);
        controller.ModelState.Clear();

        // When
        var result = await controller.UpdateAsync(id, new UpdatePersonneRequest { Nom = "Potter", Prenom = "Harry" }, default);

        // Then
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        var Personne = context.Personnes.Find(id);
        Assert.AreEqual("Potter", Personne.Nom);
        Assert.AreEqual("Harry", Personne.Prenom);
        Assert.AreEqual(4, context.Personnes.Count());
    }

    [TestMethod]
    public async Task EditerUnIdentifiantInconnuDoitRetournerUneReponse404NotFound()
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);
        var request = new UpdatePersonneRequest { Nom = "Potter", Prenom = "Harry" };

        // When
        await Assert.ThrowsExceptionAsync<NullReferenceException>(() => controller.UpdateAsync(Guid.Empty, request, default));
    }
}
