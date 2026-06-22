using HungryMonster.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HungryMonster.Infrastructure.Data.Configurations;

public class PartnerClientConfiguration : IEntityTypeConfiguration<PartnerClient>
{
    public void Configure(EntityTypeBuilder<PartnerClient> builder)
    {
        builder.Property(p => p.Industry)
            .HasMaxLength(100);
    }
}
