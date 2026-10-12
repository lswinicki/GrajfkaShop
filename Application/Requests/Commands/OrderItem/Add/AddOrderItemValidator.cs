using Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Application.Requests.Commands.OrderItem.Add;

public class AddOrderItemValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemValidator(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Order ID cannot be empty")
            .DependentRules(() =>
            {
                RuleFor(x => x.OrderId)
                    .MustAsync(async (id, cancellationToken) =>
                    {
                        var isIdExist = await orderRepository.FirstOrDefaultAsync(x => x.Id == id);
                        return isIdExist != null;
                    }).WithMessage("Order ID is not exist!");
            });

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Product ID cannot be empty")
            .DependentRules(() =>
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (id, cancellationToken) =>
                    {
                        var isIdExist = await productRepository.FirstOrDefaultAsync(x => x.Id == id);
                        return isIdExist != null;
                    }).WithMessage("Product ID is not exist!");
            });
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}