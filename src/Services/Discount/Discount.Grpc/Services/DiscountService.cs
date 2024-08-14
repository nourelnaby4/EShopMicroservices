using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountDbContext dbcontext , ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext
            .Coupons
            .FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);

        if (coupon is null)
            return new CouponModel() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        logger.LogInformation("Discount is retrived for ProductName: {productName}, Amount : {amount}",
            coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if(coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid request object"));

        dbcontext.Coupons.Add(coupon);
        await dbcontext.SaveChangesAsync();

        logger.LogInformation("Created Coupon Successfully  ProductName: {productName}, Amount : {amount}",
            coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        dbcontext.Coupons.Update(coupon);
        await dbcontext.SaveChangesAsync();

        logger.LogInformation("Updated Coupon Successfully  ProductName: {productName}, Amount : {amount}",
            coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName:{request.ProductName} is not found"));

        dbcontext.Coupons.Remove(coupon);
        await dbcontext.SaveChangesAsync();

        logger.LogInformation("Delete Coupon Successfully  ProductName: {productName}, Amount : {amount}",
            coupon.ProductName, coupon.Amount);

        return new DeleteDiscountResponse { Success = true };
    }
}
