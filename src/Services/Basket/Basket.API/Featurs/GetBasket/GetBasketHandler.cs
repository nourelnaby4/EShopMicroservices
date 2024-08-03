using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Featurs.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        return new GetBasketResult(new ShoppingCart("smn"));
    }
}
