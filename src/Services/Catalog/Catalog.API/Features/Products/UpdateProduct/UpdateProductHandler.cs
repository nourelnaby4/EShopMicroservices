namespace Catalog.API.Features.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category,string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required")
            .Length(2,150).WithMessage("Name must be length be 2 and 150");

        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");

        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be grater than zero");
    }
}

internal sealed class UpdateProductHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
