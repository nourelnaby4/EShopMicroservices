namespace Catalog.API.Features.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required")
            .Length(2, 150).WithMessage("Name must be length be 2 and 150");

        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");

        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be grater than zero");
    }
}

internal sealed class CreateProductHandler
    (IDocumentSession session , IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(command,cancellationToken);
        var errors = result.Errors.Select(x=>x.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store(product);

        await session.SaveChangesAsync();

        return new CreateProductResult(product.Id);
    }
}
