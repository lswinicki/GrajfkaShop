using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistance;

namespace Infrastructure.Repositories;

public class ContactMessageRepository : BasicRepository<ContactMessageDao, int>, IContactMessageRepository
{
    public ContactMessageRepository(ApplicationDbContext context) : base(context)
    {
    }
}
