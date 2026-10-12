using MediatR;

namespace Application.Requests.Commands.Contact.Send;

public class SendContactMessageCommand : IRequest<int>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Subject { get; set; }
    public required string Message { get; set; }
}
