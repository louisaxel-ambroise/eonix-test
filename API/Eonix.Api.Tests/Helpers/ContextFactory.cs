using Eonix.Api.Database;
using Eonix.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Eonis.Api.Tests.Helpers;

public static class ContextFactory
{
    public const string Id1 = "aebf985c-2416-422b-99e7-3a57454e2298";
    public const string Id2 = "15978191-abdd-45da-a75d-bbc87d4cc41b";
    public const string Id3 = "ad711085-88dc-4001-b932-61721ac8823b";
    public const string Id4 = "90172170-bb0f-4740-8d76-f340d69af072";

    public static EonixContext Create(string name = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EonixContext>();
        optionsBuilder.UseInMemoryDatabase(name ?? Guid.NewGuid().ToString());

        var context = new EonixContext(optionsBuilder.Options);
        context.Database.EnsureCreated();

        SeedContext(context);

        return context;
    }

    public static void SeedContext(EonixContext context)
    {
        context.Personnes.Add(new Personne { Id = Guid.Parse(Id1), Nom = "McGonagall", Prenom = "Minerva" });
        context.Personnes.Add(new Personne { Id = Guid.Parse(Id2), Nom = "Hagrid", Prenom = "Rubeus" });
        context.Personnes.Add(new Personne { Id = Guid.Parse(Id3), Nom = "Dumbledore", Prenom = "Albus" });
        context.Personnes.Add(new Personne { Id = Guid.Parse(Id4), Nom = "Malefoy", Prenom = "Drago" });

        context.SaveChanges();
    }
}
