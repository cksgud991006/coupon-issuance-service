

namespace CouponServer.Repositories;

public interface ICouponRepository
{
    public Task<bool> HasUserReceivedCoupon(string idempotencyKey);

    public bool TryIssueCoupon(int userId, string idempotencyKey);
}