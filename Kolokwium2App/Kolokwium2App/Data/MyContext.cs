using Kolokwium2App.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2App.Data;

public class MyContext : DbContext
{
    protected MyContext()
    {
    }

    public MyContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Titles> Titles { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<Characters> Characters { get; set; }
    public DbSet<CharacterTitles> CharacterTitles { get; set; }
    public DbSet<Backpacks> Backpacks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Titles>().HasData(new List<Titles>()
        {
            new Titles { Id = 1, Name = "wojownik ninja" },
            new Titles { Id = 2, Name = "kuchcik" }
        });
        modelBuilder.Entity<Items>().HasData(new List<Items>()
        {
            new Items { Id = 1, Name = "maczeta", Weight = 2 },
            new Items { Id = 2, Name = "garnek", Weight = 5 }
        });
        modelBuilder.Entity<Characters>().HasData(new List<Characters>()
        {
            new Characters { Id = 1, FirstName = "ola", LastName = "buba", CurrentWeight = 0, MaxWeight = 9 },
            new Characters { Id = 2, FirstName = "karo", LastName = "ratatuj", CurrentWeight = 4, MaxWeight = 20 },
            new Characters { Id = 3, FirstName = "zosia", LastName = "całka", CurrentWeight = 3, MaxWeight = 45 }
        });
        modelBuilder.Entity<Backpacks>().HasData(new List<Backpacks>()
        {
            new Backpacks { CharacterId = 1, ItemId = 1, Amount = 1 },
            new Backpacks { CharacterId = 2, ItemId = 2, Amount = 1 },
        });
        modelBuilder.Entity<CharacterTitles>().HasData(new List<CharacterTitles>()
        {
            new CharacterTitles { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2023-09-11") },
            new CharacterTitles { CharacterId = 2, TitleId = 2, AcquiredAt = DateTime.Parse("2020-09-11") },

        });
    }
    
}