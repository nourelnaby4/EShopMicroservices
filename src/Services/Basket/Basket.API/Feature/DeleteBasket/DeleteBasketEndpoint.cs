namespace Basket.API.Featurs.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{UserName}", async (string UserName, ISender sender) =>
        {

            var result = await sender.Send(new DeleteBasketCommand(UserName));

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok( response);
        })
          .WithName("DeleteShoppingCart")
          .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("delete Shopping Cart")
          .WithDescription("delete Shopping cart");
    }
}
