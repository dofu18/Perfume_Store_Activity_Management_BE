using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Perfume_Store_Activity_Management_Backend.src.Domain.Category;
using Perfume_Store_Activity_Management_Backend.src.Domain.Order;
using Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;
using Perfume_Store_Activity_Management_Backend.src.Domain.User;
using Perfume_Store_Activity_Management_Backend.src.Domain.Activity;
using Perfume_Store_Activity_Management_Backend.src.Domain.Payment;
using System.Composition;

namespace Perfume_Store_Activity_Management_Backend.src.Infrastructure.Database;

public class PerfumeStoreDbContext : IdentityDbContext
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PerfumeCart> PerfumeCarts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Perfume> Perfumes { get; set; }
    public DbSet<PerfumeCategory> PerfumeCategories { get; set; }
    public DbSet<PerfumeEdition> PerfumeEditions { get; set; }
    public new DbSet<Role> Roles { get; set; }
    public new DbSet<User> Users { get; set; }

    private readonly string COLLATION = "SQL_Latin1_General_CP1_CI_AI";

    public PerfumeStoreDbContext(DbContextOptions<PerfumeStoreDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Activity
        modelBuilder.Entity<Activity>()
            .HasKey(a => a.ActivityId);
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.PerfumeEdition)
            .WithMany()
            .HasForeignKey(a => a.PerfumeId);

        // Category
        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);
        modelBuilder.Entity<Category>()
            .HasMany(c => c.PerfumeCategories)
            .WithOne(pc => pc.Category)
            .HasForeignKey(pc => pc.CategoryId);

        // Cart
        modelBuilder.Entity<Cart>()
            .HasKey(c => c.CartId);
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.CreatedByUser)
            .WithMany()
            .HasForeignKey(c => c.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.PerfumeCarts)
            .WithOne(pc => pc.Cart)
            .HasForeignKey(pc => pc.CartId);

        // Order
        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderId);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Cart)
            .WithMany()
            .HasForeignKey(o => o.CartId);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.CreatedByUser)
            .WithMany()
            .HasForeignKey(o => o.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // PerfumeCart
        modelBuilder.Entity<PerfumeCart>()
            .HasKey(pc => new { pc.PerfumeId, pc.CartId });
        modelBuilder.Entity<PerfumeCart>()
            .HasOne(pc => pc.PerfumeEdition)
            .WithMany()
            .HasForeignKey(pc => pc.PerfumeId);

        // Transaction
        modelBuilder.Entity<Transaction>()
            .HasKey(t => t.TransactionId);
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Order)
            .WithMany()
            .HasForeignKey(t => t.OrderId);
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // Perfume
        modelBuilder.Entity<Perfume>()
            .HasKey(p => p.PerfumeId);
        modelBuilder.Entity<Perfume>()
            .HasMany(p => p.PerfumeEditions)
            .WithOne(pe => pe.Perfume)
            .HasForeignKey(pe => pe.PerfumeId);

        // PerfumeEdition
        modelBuilder.Entity<PerfumeEdition>()
            .HasKey(pe => pe.PerfumeEditionId);
        modelBuilder.Entity<PerfumeEdition>()
            .HasMany(pe => pe.PerfumeCategories)
            .WithOne(pc => pc.PerfumeEdition)
            .HasForeignKey(pc => pc.PerfumeId);

        // PerfumeCategory
        modelBuilder.Entity<PerfumeCategory>()
            .HasKey(pc => new { pc.PerfumeId, pc.CategoryId });

        // Role
        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleId);
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

        // User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
    }
}
