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

public class StoreBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.StoreBasket(command.Cart,cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }
}
