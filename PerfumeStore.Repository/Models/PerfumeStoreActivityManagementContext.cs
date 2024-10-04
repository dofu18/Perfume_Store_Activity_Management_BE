using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PerfumeStore.Repository.Models;

public partial class PerfumeStoreActivityManagementContext : DbContext
{
    public PerfumeStoreActivityManagementContext()
    {
    }

    public PerfumeStoreActivityManagementContext(DbContextOptions<PerfumeStoreActivityManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Perfume> Perfumes { get; set; }

    public virtual DbSet<PerfumeCart> PerfumeCarts { get; set; }

    public virtual DbSet<PerfumeEdition> PerfumeEditions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Activities_CustomerId");

            entity.HasIndex(e => e.PerfumeId, "IX_Activities_PerfumeId");

            entity.Property(e => e.ActivityId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Activities)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Perfume).WithMany(p => p.Activities).HasForeignKey(d => d.PerfumeId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_Carts_CreatedBy");

            entity.Property(e => e.CartId).ValueGeneratedNever();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_Orders_CartId");

            entity.HasIndex(e => e.CreatedBy, "IX_Orders_CreatedBy");

            entity.Property(e => e.OrderId).ValueGeneratedNever();

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders).HasForeignKey(d => d.CartId);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Perfume>(entity =>
        {
            entity.Property(e => e.PerfumeId).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PerfumeCart>(entity =>
        {
            entity.HasKey(e => new { e.PerfumeId, e.CartId });

            entity.HasIndex(e => e.CartId, "IX_PerfumeCarts_CartId");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cart).WithMany(p => p.PerfumeCarts).HasForeignKey(d => d.CartId);

            entity.HasOne(d => d.Perfume).WithMany(p => p.PerfumeCarts).HasForeignKey(d => d.PerfumeId);
        });

        modelBuilder.Entity<PerfumeEdition>(entity =>
        {
            entity.HasIndex(e => e.PerfumeId, "IX_PerfumeEditions_PerfumeId");

            entity.Property(e => e.PerfumeEditionId).ValueGeneratedNever();

            entity.HasOne(d => d.Perfume).WithMany(p => p.PerfumeEditions).HasForeignKey(d => d.PerfumeId);

            entity.HasMany(d => d.Categories).WithMany(p => p.Perfumes)
                .UsingEntity<Dictionary<string, object>>(
                    "PerfumeCategory",
                    r => r.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                    l => l.HasOne<PerfumeEdition>().WithMany().HasForeignKey("PerfumeId"),
                    j =>
                    {
                        j.HasKey("PerfumeId", "CategoryId");
                        j.ToTable("PerfumeCategories");
                        j.HasIndex(new[] { "CategoryId" }, "IX_PerfumeCategories_CategoryId");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_Transactions_CreatedBy");

            entity.HasIndex(e => e.OrderId, "IX_Transactions_OrderId");

            entity.Property(e => e.TransactionId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
