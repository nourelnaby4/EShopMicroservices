namespace Basket.API.Featurs.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidatot : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidatot()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("cart can not be null");

        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = request.Cart;

        //TODO: store in database
        //TODO: update cache

        return new StoreBasketResult("sms");
    }
}
