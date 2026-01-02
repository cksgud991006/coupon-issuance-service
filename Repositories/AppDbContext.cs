using Microsoft.EntityFrameworkCore;
using CouponServer.Domain.Coupons;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace CouponServer.Repositories;
public class AppDbContext: DbContext
{

    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<CouponPolicy> CouponPolicy { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(c => c.CouponId);

            entity.Property(c => c.UserId)
                .IsRequired();

            entity.HasIndex(c => c.IdempotencyKey)
                .IsUnique();
        });

        modelBuilder.Entity<CouponPolicy>(entity =>
        {
            entity.HasIndex(cp => cp.Id)
                .IsUnique();

           entity.Property(cp => cp.TotalQuantity)
            .HasDefaultValue(1);
        });
    }
}