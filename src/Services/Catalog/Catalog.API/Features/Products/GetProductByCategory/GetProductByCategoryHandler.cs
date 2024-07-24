using Marten.Linq.QueryHandlers;

namespace Catalog.API.Features.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler. Handle called with {@Query}", query);

        var products = await session.Query<Product>()
            .Where(x=>x.Category.Contains(query.Category))
            .ToListAsync();

        return new GetProductByCategoryResult(products);
    }
}
