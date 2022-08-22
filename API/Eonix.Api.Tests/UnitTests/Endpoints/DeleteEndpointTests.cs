using Eonix.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eonis.Api.Tests;

[TestClass]
public class DeleteEndpointTests
{
    [TestMethod]
    public async Task SupprimerUnePersonneDoitRetournerUneReponse204NoContent()
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);
        var id = Guid.Parse(ContextFactory.Id1);
        controller.ModelState.Clear();

        // When
        var result = await controller.DeleteAsync(id, default);

        // Then
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        Assert.IsNull(context.Personnes.Find(id));
        Assert.AreEqual(3, context.Personnes.Count());
    }

    [TestMethod]
    public async Task SupprimerUnIdentifiantInconnuDoitRetournerUneReponse404NotFound()
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);
        controller.ModelState.Clear();

        // When
        await Assert.ThrowsExceptionAsync<NullReferenceException>(() => controller.DeleteAsync(Guid.Empty, default));
    }
}
