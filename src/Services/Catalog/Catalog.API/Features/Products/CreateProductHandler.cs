namespace Catalog.API.Features.Products;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record CreateProductResult(Guid Id);

public class CreateProductHandler
{
}
