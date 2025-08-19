using Application.Requests.Commands.Auth.Register;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDao, RegisterCommand>().ReverseMap();
    }
}