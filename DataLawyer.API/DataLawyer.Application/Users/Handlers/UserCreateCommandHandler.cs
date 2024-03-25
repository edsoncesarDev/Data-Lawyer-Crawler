
using AutoMapper;
using DataLawyer.Application.Users.Commands;
using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using MediatR;

namespace DataLawyer.Application.Users.Handlers;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, User>
{
    private readonly IGeneralPersistence _persistence;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public UserCreateCommandHandler(IGeneralPersistence persistence, IUser user, IMapper mapper)
    {
        _persistence = persistence;
        _user = user;
        _mapper = mapper;
    }

    public async Task<User> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var user = await _user.UserExists(request.Email, request.Password);

        if (user != null)
            DomainValidationExceptions.When(true, "Existing user.");

        var userDomain = _mapper.Map<User>(request);

        userDomain.PasswordEncryption(request.Password);

        _persistence.Add(userDomain);

        await _persistence.SaveChangesAsync();

        return await _user.GetUserByIdAsync(userDomain.Id);
    }

    
}
