using Application.Requests.Commands.Contact.Send;
using Application.Requests.Queries.Contact.GetContactMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Send([FromBody] SendContactMessageCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetContactMessagesQuery());
        return Ok(result);
    }
}
