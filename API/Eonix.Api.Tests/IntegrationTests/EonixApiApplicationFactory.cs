using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Eonix.Api.Tests.IntegrationTests;

class EonixApiApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Integration.json")
                .Build();

            config.AddConfiguration(Configuration);
        });
    }
}
