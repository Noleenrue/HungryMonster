using HungryMonster.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HungryMonster.Infrastructure.Data.Configurations;

public class MealRecordConfiguration : IEntityTypeConfiguration<MealRecord>
{
    public void Configure(EntityTypeBuilder<MealRecord> builder)
    {
        builder.ToTable("MealRecords");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd();

        builder.Property(m => m.Year)
            .IsRequired();

        builder.Property(m => m.NumberOfServings)
            .IsRequired();

        builder.Property(m => m.CreatedAt)
            .IsRequired();

        builder.Property(m => m.UpdatedAt);

        // One-to-many: Client -> MealRecords
        builder.HasOne(m => m.Client)
            .WithMany(c => c.MealRecords)
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
