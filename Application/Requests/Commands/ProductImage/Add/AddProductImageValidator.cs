using FluentValidation;

namespace Application.Requests.Commands.ProductImage.Add;

public class AddProductImageValidator : AbstractValidator<AddProductImageCommand>
{
    public AddProductImageValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ID produktu jest wymagane.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("URL zdjęcia jest wymagany.");
    }
}
