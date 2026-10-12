using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.Contact.Send;

public class SendContactMessageHandler(IContactMessageRepository contactMessageRepository) 
    : IRequestHandler<SendContactMessageCommand, int>
{
    public async Task<int> Handle(SendContactMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new ContactMessageDao
        {
            Name = request.Name,
            Email = request.Email,
            Subject = request.Subject,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };

        return await contactMessageRepository.AddAsync(message, cancellationToken);
    }
}
