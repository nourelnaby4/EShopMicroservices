namespace Catalog.API.Features.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}",
            async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
          .WithName("DeleteProduct")
          .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Delete Product")
          .WithDescription("Delete Product");
    }
}