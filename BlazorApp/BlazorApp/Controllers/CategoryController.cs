using Application.Requests.Commands.Category.Add;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<int>> Add([FromBody] AddCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
}