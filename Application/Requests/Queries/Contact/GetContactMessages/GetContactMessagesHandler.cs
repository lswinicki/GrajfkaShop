using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Requests.Queries.Contact.GetContactMessages;

public class GetContactMessagesHandler(IContactMessageRepository contactMessageRepository)
    : IRequestHandler<GetContactMessagesQuery, IEnumerable<ContactMessageDto>>
{
    public async Task<IEnumerable<ContactMessageDto>> Handle(GetContactMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await contactMessageRepository.GetAllAsync(asNoTracking: true, cancellationToken);

        return messages
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new ContactMessageDto
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Subject = m.Subject,
                Message = m.Message,
                CreatedAt = m.CreatedAt,
                IsRead = m.IsRead
            });
    }
}
