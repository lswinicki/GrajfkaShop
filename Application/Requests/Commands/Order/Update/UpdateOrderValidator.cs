using Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Application.Requests.Commands.Order.Update;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator(IOrderRepository orderRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id cannot be empty!")
            .DependentRules(() =>
                RuleFor(x => x.Id).MustAsync(async (id, cancellationToken) =>
                {
                    var isIdExist = await orderRepository.FirstOrDefaultAsync(x => x.Id == id);
                    return isIdExist != null;
                }).WithMessage("Order id does not exists!"));
        
        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order date cannot be empty!");
        
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status is not valid!");
    }
}