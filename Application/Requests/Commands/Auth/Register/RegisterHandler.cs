using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Commands.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<UserDao> _userManager;
    private readonly IMapper _mapper;
    
    public RegisterHandler(UserManager<UserDao> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<UserDao>(request);
        
        var addNewUser = await _userManager.CreateAsync(newUser, request.Password);
        var addRole = await _userManager.AddToRoleAsync(newUser, "User");
        
        return addNewUser.Succeeded && addRole.Succeeded;
    }
}