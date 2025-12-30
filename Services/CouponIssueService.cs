using CouponServer.Repositories;
using Domain.Coupons;

namespace CouponServer.Services;

public class CouponService
{
    private readonly CouponRepository _couponRepository;

    public CouponService(CouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CouponIssueResult> IssueAsync(
        int userId,
        string idempotencyKey)
    {

        if (await _couponRepository.HasUserReceivedCoupon(idempotencyKey))
        {
            return CouponIssueResult.AlreadyIssued();
        }

        bool issued = _couponRepository.TryIssueCoupon(userId, idempotencyKey); 
        if (!issued)
        {
            return CouponIssueResult.SoldOut();
        }

        return CouponIssueResult.Success();
    }
}