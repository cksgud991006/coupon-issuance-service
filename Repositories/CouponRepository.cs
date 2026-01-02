using Microsoft.EntityFrameworkCore;
using CouponServer.Domain.Coupons;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Diagnostics.CodeAnalysis;
using System.Data.SqlTypes;
using System.Security.Cryptography;

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
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CanIssueCoupon()
    {
        var issued = await _context.Coupons.CountAsync();
        var limit = await _context.CouponPolicy
            .Select(cp => cp.TotalQuantity)
            .SingleAsync();

        if (issued >= limit) return false;

        return true;
    }
}