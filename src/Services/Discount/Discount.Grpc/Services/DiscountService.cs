using Discount.Grpc.Data;
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

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}
