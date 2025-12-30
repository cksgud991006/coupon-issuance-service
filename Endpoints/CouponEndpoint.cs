using CouponServer.Services;

namespace CouponServer.Endpoints;

public static class CouponEndpoint
{
    public static void MapCouponEndPoints(this WebApplication app)
    {
        // GET 
        app.MapGet("/coupons/users/{userId}", GetCouponByUserId);
        app.MapGet("/coupons/{couponId}", GetCouponByCouponId);
        
        // POST
        app.MapPost("/coupons/issue", IssueCoupon);
    }

    private static async Task<IResult> GetCouponByUserId(
        int userId)
    {
        return Results.Ok();
    } 

    private static async Task<IResult> GetCouponByCouponId(
        int couponId)
    {
        return Results.Ok();
    } 

    private static async Task<IResult> IssueCoupon(
        int userId,
        string idempotencyKey,
        CouponService service)
    {
        await service.IssueAsync(userId, idempotencyKey);
        return Results.Ok();
    }
}