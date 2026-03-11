using Microsoft.EntityFrameworkCore;
using BridgeInsight.Models;

namespace BridgeInsight.Data;

public class BridgeDbContext : DbContext
{
    public DbSet<Bridge> Bridges => Set<Bridge>();

    public BridgeDbContext(DbContextOptions<BridgeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bridge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StructureNumber).IsUnique();
            entity.HasIndex(e => e.CountyCode);
            entity.HasIndex(e => e.FacilityCarried);
            entity.HasIndex(e => e.DeckCondition);
            entity.HasIndex(e => e.SuperstructureCondition);
            entity.HasIndex(e => e.SubstructureCondition);

            entity.Property(e => e.StructureNumber).HasMaxLength(15);
            entity.Property(e => e.StateCode).HasMaxLength(3);
            entity.Property(e => e.CountyCode).HasMaxLength(3);
            entity.Property(e => e.FeaturesIntersected).HasMaxLength(100);
            entity.Property(e => e.FacilityCarried).HasMaxLength(100);
            entity.Property(e => e.CountyName).HasMaxLength(50);

            // Ignore computed properties
            entity.Ignore(e => e.Age);
            entity.Ignore(e => e.LowestConditionRating);
            entity.Ignore(e => e.OverallCondition);
            entity.Ignore(e => e.IsStructurallyDeficient);
            entity.Ignore(e => e.IsInspectionOverdue);
        });
    }
}
