namespace Catalog.API.Features.Products.UpdateProduct;

public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResponse(Guid Id);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",
            async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok( response);
            })
          .WithName("UpdateProduct")
          .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Update Product")
          .WithDescription("Update Product");
    }
}