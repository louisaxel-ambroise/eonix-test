using Eonix.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Eonix.Api.Database;

public class EonixContext : DbContext
{
    public EonixContext(DbContextOptions<EonixContext> options) : base(options)
    {
    }

    public DbSet<Personne> Personnes { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var personneEntity = modelBuilder.Entity<Personne>(p =>
        {
            p.HasKey(x => x.Id);
            p.Property(x => x.Nom).HasMaxLength(100).IsRequired();
            p.Property(x => x.Prenom).HasMaxLength(100).IsRequired();
        });
    }
}
