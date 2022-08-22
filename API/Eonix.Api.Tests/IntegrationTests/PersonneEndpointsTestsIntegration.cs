using Eonix.Api.Model;
using Eonix.Api.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eonis.Api.Tests.IntegrationTests;
 
[TestClass]
public class PersonneEndpointsTestsIntegration : IntegrationTest
{
    [TestMethod]
    public async Task GetRetourneLesPersonnesEnregistrees()
    {
        // When
        var response = await _client.GetAsync("/api/Personne");

        // Then
        Assert.AreEqual(200, (int) response.StatusCode);
        Assert.AreEqual(4, JsonConvert.DeserializeObject<Personne[]>(await response.Content.ReadAsStringAsync()).Length);
    }

    [TestMethod]
    public async Task GetFiltreParNomRetourneLesPersonnesEnregistrees()
    {
        // When
        var response = await _client.GetAsync("/api/Personne?nom=m");

        // Then
        Assert.AreEqual(200, (int) response.StatusCode);
        var Personnes = JsonConvert.DeserializeObject<Personne[]>(await response.Content.ReadAsStringAsync());
        Assert.AreEqual(2, Personnes.Length);
        Assert.IsTrue(Personnes.All(x => x.Nom.StartsWith("m", StringComparison.OrdinalIgnoreCase) || x.Nom.EndsWith("m", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task GetFiltreParPrenomRetourneLesPersonnesEnregistrees()
    {
        // When
        var response = await _client.GetAsync("/api/Personne?prenom=a");

        // Then
        Assert.AreEqual(200, (int)response.StatusCode);
        var Personnes = JsonConvert.DeserializeObject<Personne[]>(await response.Content.ReadAsStringAsync());
        Assert.AreEqual(2, Personnes.Length);
        Assert.IsTrue(Personnes.All(x => x.Prenom.StartsWith("a", StringComparison.OrdinalIgnoreCase) || x.Prenom.EndsWith("a", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task GetParIdRetourneUnePersonne()
    {
        // When
        var response = await _client.GetAsync($"/api/Personne/{ContextFactory.Id1}");

        // Then
        Assert.AreEqual(200, (int)response.StatusCode);
        var Personne = JsonConvert.DeserializeObject<Personne>(await response.Content.ReadAsStringAsync());
        Assert.IsNotNull(Personne);
    }

    [TestMethod]
    public async Task GetAvecUnIdentifiantInvalideRetourneUneErreur404()
    {
        // When
        var response = await _client.GetAsync($"/api/Personne/{Guid.Empty}");

        // Then
        Assert.AreEqual(404, (int)response.StatusCode);
    }

    [TestMethod]
    public async Task PostAjouteUneNouvellePersonne()
    {
        // Given
        var payload = new CreatePersonneRequest { Nom = "Potter", Prenom = "Harry" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PostAsync("/api/Personne", content);

        // Then
        Assert.AreEqual(201, (int) response.StatusCode);
        var Personne = JsonConvert.DeserializeObject<Personne>(await response.Content.ReadAsStringAsync());
        Assert.AreEqual("Harry", Personne.Prenom);
        Assert.AreEqual("Potter", Personne.Nom);
    }

    [TestMethod]
    public async Task PostAvecUnNomInvalideRetourneUneErreur400()
    {
        // Given
        var payload = new CreatePersonneRequest { Nom = "", Prenom = "Harry" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PostAsync("/api/Personne", content);

        // Then
        Assert.AreEqual(400, (int) response.StatusCode);
    }

    [TestMethod]
    public async Task PostAvecUnPrenomInvalideRetourneUneErreur400()
    {
        // Given
        var payload = new CreatePersonneRequest { Nom = "Potter", Prenom = "" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PostAsync("/api/Personne", content);

        // Then
        Assert.AreEqual(400, (int) response.StatusCode);
    }

    [TestMethod]
    public async Task PutModifieUnePersonne()
    {
        // Given
        var payload = new UpdatePersonneRequest { Nom = "Potter", Prenom = "Harry" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PutAsync($"/api/Personne/{ContextFactory.Id1}", content);

        // Then
        Assert.AreEqual(204, (int) response.StatusCode);
    }

    [TestMethod]
    public async Task PutAvecUnNomInvalideRetourneUneErreur400()
    {
        // Given
        var payload = new UpdatePersonneRequest { Nom = "", Prenom = "Harry" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PutAsync($"/api/Personne/{ContextFactory.Id1}", content);

        // Then
        Assert.AreEqual(400, (int)response.StatusCode);
    }

    [TestMethod]
    public async Task PutAvecUnPrenomInvalideRetourneUneErreur400()
    {
        // Given
        var payload = new UpdatePersonneRequest { Nom = "Potter", Prenom = "" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PutAsync($"/api/Personne/{ContextFactory.Id1}", content);

        // Then
        Assert.AreEqual(400, (int)response.StatusCode);
    }

    [TestMethod]
    public async Task PutAvecUnIdInexistantRetourneUneErreur404()
    {
        // Given
        var payload = new UpdatePersonneRequest { Nom = "Potter", Prenom = "Harry" };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // When
        var response = await _client.PutAsync($"/api/Personne/{Guid.Empty}", content);

        // Then
        Assert.AreEqual(404, (int)response.StatusCode);
    }

    [TestMethod]
    public async Task DeleteSupprimeUnePersonne()
    {
        // When
        var response = await _client.DeleteAsync($"/api/Personne/{ContextFactory.Id1}");

        // Then
        Assert.AreEqual(204, (int)response.StatusCode);
    }

    [TestMethod]
    public async Task DeleteAvecUnIdInvalideRetourneUneErreur404()
    {
        // When
        var response = await _client.DeleteAsync($"/api/Personne/{Guid.Empty}");

        // Then
        Assert.AreEqual(404, (int)response.StatusCode);
    }
}
