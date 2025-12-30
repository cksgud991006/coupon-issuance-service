namespace Domain.Coupons;

public class Coupon
{
    public int UserId { get; set; }
    public int CouponId { get; set; }
    public string IdempotencyKey { get; set; }
    
    private Coupon() { }

    public static Coupon Create(int userId, string idempotencyKey)
    {
        return new Coupon
        {
            UserId = userId,
            IdempotencyKey = idempotencyKey
        };
    }
}