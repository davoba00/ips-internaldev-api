using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RebuildProject.Models;

public partial class AppDbContext : DbContext
{
    #region Constructors

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    #endregion

    #region DbSet

    public virtual DbSet<Resource> Resources { get; set; }
    public virtual DbSet<ResourceItem> ResourceItems { get; set; }
    public virtual DbSet<ApiLog> ApiLogs { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasQueryFilter(e => e.Deleted == null);

            entity.ToTable("Resource", "dbo");

            entity.HasKey(e => e.ResourceId);

            entity.Property(e => e.ResourceId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ResourceItem>(entity =>
        {
            entity.HasQueryFilter(e => e.Deleted == null);

            entity.ToTable("ResourceItem", "dbo");

            entity.HasKey(e => e.ResourceItemId);

            entity.HasIndex(e => e.ResourceId, "IX_ResourceItems_ResourceId");

            entity.Property(e => e.ResourceItemId).ValueGeneratedNever();

            entity.HasOne(d => d.Resource).WithMany(p => p.ResourceItems)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<ApiLog>(entity =>
        {
            entity.ToTable("ApiLog", "dbo");

            entity.HasKey(e => e.LogId);

            entity.Property(e => e.LogId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
