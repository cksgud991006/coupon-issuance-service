using CouponServer.Repositories;
using CouponServer.Domain.Coupons;

namespace CouponServer.Services;

public class CouponIssueService: ICouponService
{
    private readonly ICouponRepository _couponRepository;

    public CouponIssueService(ICouponRepository couponRepository)
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

        bool issued = await _couponRepository.TryIssueCoupon(userId, idempotencyKey); 
        if (!issued)
        {
            return CouponIssueResult.SoldOut();
        }

        return CouponIssueResult.Success();
    }
}