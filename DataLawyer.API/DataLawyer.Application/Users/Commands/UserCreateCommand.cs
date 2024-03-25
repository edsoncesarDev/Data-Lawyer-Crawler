
using DataLawyer.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DataLawyer.Application.Users.Commands;

public class UserCreateCommand : IRequest<User>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
