using Eonix.Api.Controllers;
using Eonix.Api.Contracts;
using Eonis.Api.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eonis.Api.Tests.Endpoints;

[TestClass]
public class ListEndpointTests
{
    [TestMethod]
    [DataRow("M", null, 2)]
    [DataRow("m", null, 2)]
    [DataRow("Mc", null, 1)]
    [DataRow("aga", null, 0)]
    [DataRow("all", "\t", 1)]
    [DataRow(null, "a", 2)]
    [DataRow(null, "Dra", 1)]
    [DataRow(null, "lbu", 0)]
    [DataRow("", "", 4)]
    [DataRow("   ", null, 4)]
    [DataRow("\t", null, 4)]
    [DataRow(null, null, 4, DisplayName = "NoFilter")]
    public async Task LaReponseDoitContenirLesPersonnesQuiCorrespondentAuxFiltres(string nom, string prenom, int expectation)
    {
        // Given
        var context = ContextFactory.Create();
        var controller = new PersonneController(context);

        // When
        var result = await controller.ListAsync(new PersonneFilter { Prenom = prenom, Nom = nom }, default) as OkObjectResult;

        // Then
        Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<PersonneResponse>));
        var Personnes = (IEnumerable<PersonneResponse>)result.Value;
        Assert.AreEqual(expectation, Personnes.Count());
    }
}
