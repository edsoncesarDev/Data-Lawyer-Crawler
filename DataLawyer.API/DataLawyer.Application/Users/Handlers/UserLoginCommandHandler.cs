
using DataLawyer.Application.DTOs;
using DataLawyer.Application.Users.Commands;
using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DataLawyer.Application.Users.Handlers;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserTokenDto>
{
    private readonly IUser _user;
    private readonly IConfiguration _configuration;

    public UserLoginCommandHandler(IUser user, IConfiguration configuration)
    {
        _user = user;
        _configuration = configuration;
    }

    public async Task<UserTokenDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _user.UserExists(request.Email, PasswordEncryption(request.Password));

        if (user is null)
            DomainValidationExceptions.When(true, "Nonexistent user.");

        return GenerateToken(user!);
    }

    private string PasswordEncryption(string password)
    {
        byte[] dataBytes = Encoding.ASCII.GetBytes(password);
        using (SHA512 sha512 = new SHA512Managed())
        {
            byte[] hashBytes = sha512.ComputeHash(dataBytes);
            string hash = Convert.ToBase64String(hashBytes);
            return hash;
        }
    }

    private UserTokenDto GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim("Email", user.Email),
            new Claim("NewValue", "DataLawyerWebAPI"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(2);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserTokenDto(new JwtSecurityTokenHandler().WriteToken(token), expiration);
    }


}
