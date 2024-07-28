using Marten.Linq.QueryHandlers;

namespace Catalog.API.Features.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal sealed class GetProductByCategoryHandler(IDocumentSession session) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(x=>x.Category.Contains(query.Category))
            .ToListAsync();

        return new GetProductByCategoryResult(products);
    }
}
