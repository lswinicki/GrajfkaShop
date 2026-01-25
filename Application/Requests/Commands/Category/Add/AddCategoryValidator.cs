using Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Application.Requests.Commands.Category.Add;

public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryValidator(ICategoryRepository categoryRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Category name cannot be empty!");

        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellationToken) =>
            {
                var isNameExist = await categoryRepository.FirstOrDefaultAsync(x => x.Name == name);
                return isNameExist == null;
            }).WithMessage("Category name already exists!");
    }
}