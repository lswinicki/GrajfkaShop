using FluentValidation;

namespace Application.Requests.Commands.Products.Update;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID produktu jest wymagane.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nazwa produktu jest wymagana.")
            .MaximumLength(200).WithMessage("Nazwa produktu nie może przekraczać 200 znaków.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Cena musi być większa niż 0.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kategoria jest wymagana.");
    }
}
