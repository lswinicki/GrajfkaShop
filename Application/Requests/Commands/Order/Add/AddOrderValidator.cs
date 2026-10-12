using FluentValidation;

namespace Application.Requests.Commands.Order.Add;

public class AddOrderValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderValidator()
    {
        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order date cannot be empty!");
        
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status is not valid!");
    }
}