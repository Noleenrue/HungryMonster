using HungryMonster.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HungryMonster.Infrastructure.Data;

public class HungryMonsterDbContext : DbContext
{
    public HungryMonsterDbContext(DbContextOptions<HungryMonsterDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ContractorClient> ContractorClients => Set<ContractorClient>();
    public DbSet<PartnerClient> PartnerClients => Set<PartnerClient>();
    public DbSet<MealRecord> MealRecords => Set<MealRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HungryMonsterDbContext).Assembly);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var seed = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // 3 Contractor clients
        modelBuilder.Entity<ContractorClient>().HasData(
            new { Id = 1, Name = "BuildRight Ltd",    CompanyNumber = "CRN001", CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 2, Name = "ConstructCo",       CompanyNumber = "CRN002", CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 3, Name = "SteelWorks Inc",    CompanyNumber = "CRN003", CreatedAt = seed, UpdatedAt = (DateTime?)null }
        );

        // 2 Partner clients
        modelBuilder.Entity<PartnerClient>().HasData(
            new { Id = 4, Name = "GreenLeaf Partners", Industry = "Agriculture", CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 5, Name = "TechBridge Corp",    Industry = "Technology",  CreatedAt = seed, UpdatedAt = (DateTime?)null }
        );

        // Sample meal records spread across different years
        modelBuilder.Entity<MealRecord>().HasData(
            new { Id = 1,  Year = 2022, NumberOfServings = 120, ClientId = 1, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 2,  Year = 2022, NumberOfServings = 85,  ClientId = 2, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 3,  Year = 2022, NumberOfServings = 200, ClientId = 4, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 4,  Year = 2023, NumberOfServings = 150, ClientId = 1, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 5,  Year = 2023, NumberOfServings = 95,  ClientId = 2, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 6,  Year = 2023, NumberOfServings = 310, ClientId = 3, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 7,  Year = 2023, NumberOfServings = 175, ClientId = 4, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 8,  Year = 2023, NumberOfServings = 260, ClientId = 5, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 9,  Year = 2024, NumberOfServings = 400, ClientId = 3, CreatedAt = seed, UpdatedAt = (DateTime?)null },
            new { Id = 10, Year = 2024, NumberOfServings = 220, ClientId = 5, CreatedAt = seed, UpdatedAt = (DateTime?)null }
        );
    }
}
