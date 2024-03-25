
using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Infrastructure.Repository;

public class MovementPersistence : IMovement
{
    private readonly AppDbContext _context;

    public MovementPersistence(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movement>> GetAllMovementsAsync()
    {
        return await _context.Movements.AsNoTracking().ToListAsync();
    }

    public async Task<Movement> GetMovementByIdAsync(int id)
    {
        return await _context.Movements.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> MovementExistsAsync(string movement)
    {
        return await _context.Movements.AsNoTracking().Where(x => x.TheMovement == movement).AnyAsync();
    }
}
