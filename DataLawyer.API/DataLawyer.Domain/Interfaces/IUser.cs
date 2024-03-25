using DataLawyer.Domain.Entities;

namespace DataLawyer.Domain.Interfaces;

public interface IUser
{
    Task<IEnumerable<User>> GetAllUserAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> UserExists(string email, string password);
}
