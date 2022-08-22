using Eonix.Api.Database;
using Eonix.Api.Tests.IntegrationTests;
using Eonis.Api.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Eonis.Api.Tests.IntegrationTests;

public abstract class IntegrationTest
{
    private readonly EonixApiApplicationFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest()
    {
        _factory = new ();

        using(var scope = _factory.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<EonixContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            ContextFactory.SeedContext(context);
        }

        _client = _factory.CreateClient();
    }
}
