using Eonix.Api.Controllers;
using Eonix.Api.Contracts;
using Eonix.Api.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Eonix.Api.Tests.Endpoints;

[TestClass]
public class PostEndpointTests
{
    [TestMethod]
    public async Task CreerUnePersonneDoitRetournerUneReponse201Created()
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);
        controller.ModelState.Clear();

        // When
        var result = await controller.CreateAsync(new CreatePersonneRequest { Nom = "Potter", Prenom = "Harry" }, default) as CreatedAtActionResult;

        // Then
        Assert.IsInstanceOfType(result.Value, typeof(PersonneResponse));
        var personne = result.Value as PersonneResponse;
        Assert.AreEqual("Potter", personne.Nom);
        Assert.AreEqual("Harry", personne.Prenom);
        Assert.AreEqual(5, context.Personnes.Count());
        Assert.IsNotNull(context.Personnes.Find(personne.Id));
    }
}
