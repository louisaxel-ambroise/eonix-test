namespace Eonix.Api.Database;

public static class DatabaseMigrator
{
    public static IApplicationBuilder ApplyMygrations(this IApplicationBuilder application)
    {
        using var scope = application.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<EonixContext>();

        context.Database.EnsureCreated();

        return application;
    }
}
