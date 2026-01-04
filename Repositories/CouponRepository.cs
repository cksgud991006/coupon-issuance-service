using Microsoft.EntityFrameworkCore;
using CouponServer.Domain.Coupons;
using System.Data.SqlTypes;

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


    public async Task<bool> TryIssueCoupon(int userId, string idempotencyKey)
    {
        var coupon = Coupon.Create(userId, idempotencyKey);
        _context.Coupons.Add(coupon);

        var affected = await _context.CouponPolicy
            .Where(p => p.IssuedCoupons < p.TotalQuantity)
            .ExecuteUpdateAsync(p =>
                p.SetProperty(
                    x => x.IssuedCoupons,
                    x => x.IssuedCoupons + 1
                )
            );

        if (affected == 0)
            return false;
        
        await _context.SaveChangesAsync();
        return true;
    }
}