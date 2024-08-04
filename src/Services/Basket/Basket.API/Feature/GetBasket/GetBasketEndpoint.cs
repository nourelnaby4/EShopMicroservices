using Basket.API.Models;

namespace Basket.API.Featurs.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(UserName));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
          .WithName("GetShoppingCart")
          .Produces<GetBasketResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Shopping Cart by username")
          .WithDescription("Get Shopping Cart by username");
    }
}
