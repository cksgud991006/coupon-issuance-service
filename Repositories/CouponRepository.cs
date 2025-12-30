using Microsoft.EntityFrameworkCore;
using Domain.Coupons;

namespace CouponServer.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;

    public CouponRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> HasUserReceivedCoupon(string idempotencyKey)
    {
        return _context.Coupons
            .AnyAsync(c => c.IdempotencyKey == idempotencyKey);
    }

    public bool TryIssueCoupon(int userId, string idempotencyKey)
    {
        var coupon = Coupon.Create(userId, idempotencyKey);
        _context.Coupons.Add(coupon);

        return _context.SaveChanges() > 0;
    }
}