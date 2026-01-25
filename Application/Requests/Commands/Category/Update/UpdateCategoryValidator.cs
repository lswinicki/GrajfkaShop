using Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Application.Requests.Commands.Category.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator(ICategoryRepository categoryRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Category name cannot be empty!");
        
        RuleFor(x => x)
            .MustAsync(async (command, cancellationToken) =>
            {
                var isNameExist = await categoryRepository.FirstOrDefaultAsync(x => x.Name == command.Name && x.Id != command.Id);
                return isNameExist == null;
            }).WithMessage("Category name already exists!");

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id cannot be empty!")
            .DependentRules(() =>
                RuleFor(x => x.Id).MustAsync(async (id, cancellationToken) =>
                {
                    var isIdExist = await categoryRepository.FirstOrDefaultAsync(x => x.Id == id);
                    return isIdExist != null;
                }).WithMessage("Category id does not exists!"));
    }
}
