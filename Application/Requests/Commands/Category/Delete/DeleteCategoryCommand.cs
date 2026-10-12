using MediatR;

namespace Application.Requests.Commands.Category.Delete;

public class DeleteCategoryCommand : IRequest<int>
{
    public int Id { get; set; }
}
