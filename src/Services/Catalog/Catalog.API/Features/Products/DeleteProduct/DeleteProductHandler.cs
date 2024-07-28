namespace Catalog.API.Features.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id)
    : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

internal sealed class DeleteProductHandler
    (IDocumentSession session, ILogger<DeleteProductHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler. Handle Delete called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
