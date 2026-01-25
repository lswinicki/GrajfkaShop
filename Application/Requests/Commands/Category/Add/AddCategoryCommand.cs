using Application.Requests.Commands.Category.Models;
using MediatR;

namespace Application.Requests.Commands.Category.Add;

public class AddCategoryCommand : IRequest<int>
{
    public required string Name { get; set; }
}