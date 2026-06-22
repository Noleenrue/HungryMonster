using HungryMonster.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HungryMonster.Infrastructure.Data.Configurations;

public class ContractorClientConfiguration : IEntityTypeConfiguration<ContractorClient>
{
    public void Configure(EntityTypeBuilder<ContractorClient> builder)
    {
        builder.Property(c => c.CompanyNumber)
            .HasMaxLength(50);
    }
}
