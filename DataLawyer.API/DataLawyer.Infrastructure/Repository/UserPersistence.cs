
using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Infrastructure.Repository;

public class UserPersistence : IUser
{
    private readonly AppDbContext _context;

    public UserPersistence(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUserAsync() => await _context.Users.AsNoTracking().ToListAsync();

    public async Task<User> GetUserByIdAsync(int id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User> UserExists(string email, string password)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}
