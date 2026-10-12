using Application.Requests.Commands.Category.Add;
using Application.Requests.Commands.Category.Delete;
using Application.Requests.Commands.Category.Update;
using Application.Requests.Queries.Categories.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Add([FromBody] AddCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Update([FromBody] UpdateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await _mediator.Send(new DeleteCategoryCommand { Id = id });
    }
}