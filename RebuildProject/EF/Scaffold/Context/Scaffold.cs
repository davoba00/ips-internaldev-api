using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RebuildProject.Models;

namespace RebuildProject.EF.Scaffold.Context;

public partial class Scaffold : DbContext
{
    public Scaffold()
    {
    }

    public Scaffold(DbContextOptions<Scaffold> options)
        : base(options)
    {
    }

    public virtual DbSet<ResourceCapacity> ResourceCapacities { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResourceCapacity>(entity =>
        {
            entity.ToTable("ResourceCapacity");

            entity.Property(e => e.ResourceCapacityId).ValueGeneratedNever();
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasColumnType("datetime");
            entity.Property(e => e.Updated).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
