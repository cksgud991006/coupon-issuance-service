namespace Domain.Coupons;

public class CouponIssueResult
{
    public bool IsSuccess { get; }
    public string Reason { get; }

    private CouponIssueResult(bool success, string reason)
    {
        IsSuccess = success;
        Reason = reason;
    }

    public static CouponIssueResult Success() 
        => new CouponIssueResult(true, "SUCCESS");
    public static CouponIssueResult AlreadyIssued()
        => new CouponIssueResult(false, "ALREADY_ISSUED");
    public static CouponIssueResult SoldOut() 
        => new CouponIssueResult(false, "SOLD_OUT");
    
}