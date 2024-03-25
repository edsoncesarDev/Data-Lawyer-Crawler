using DataLawyer.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DataLawyer.Application.Users.Commands;

public class UserLoginCommand : IRequest<UserTokenDto>
{
    [EmailAddress]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
