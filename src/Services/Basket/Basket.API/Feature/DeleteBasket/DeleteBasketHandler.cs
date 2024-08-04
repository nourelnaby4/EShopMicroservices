
namespace Basket.API.Featurs.DeleteBasket;

public  record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x=>x.UserName).NotEmpty().WithMessage("Username is required");
    }
}

internal sealed class DeleteBasketCommandHandler
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // TODO: delete basket from database and cache
        // session.delete in dataase

        return new DeleteBasketResult(true);
    }
}
