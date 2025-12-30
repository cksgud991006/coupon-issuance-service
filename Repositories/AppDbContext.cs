using Microsoft.EntityFrameworkCore;
using Domain.Coupons;

namespace CouponServer.Repositories;
public class AppDbContext: DbContext
{

    public DbSet<Coupon> Coupons { get; set; }

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
    }
}