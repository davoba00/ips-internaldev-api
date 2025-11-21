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
    public virtual DbSet<ResourceAssignment> ResourceAssignments { get; set; }

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

        modelBuilder.Entity<ResourceAssignment>(entity =>
        {
            entity.HasQueryFilter(e => e.Deleted == null);

            entity.ToTable("ResourceAssignment", "dbo");

            entity.HasKey(e => e.ResourceAssignmentId);

            entity.Property(e => e.ResourceAssignmentId).ValueGeneratedNever();

            entity.HasIndex(e => e.ResourceId);

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasColumnType("datetime");
            entity.Property(e => e.Updated).HasColumnType("datetime");

            //entity.HasOne(d => d.Resource)
            //    .WithMany(p => p.ResourceAssignment)
            //    .HasForeignKey(d => d.ResourceId)
            //    .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
