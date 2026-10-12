using FluentValidation;

namespace Application.Requests.Commands.Products.Add;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nazwa produktu jest wymagana.")
            .MaximumLength(200).WithMessage("Nazwa produktu nie może przekraczać 200 znaków.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Cena musi być większa niż 0.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kategoria jest wymagana.");
    }
}
