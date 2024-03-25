
using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Infrastructure.Repository;

public class AreaPersistence : IArea
{
    private readonly AppDbContext _context;

    public AreaPersistence(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Area>> GetAllAreasAsync()
    {
        return await _context.Areas.AsNoTracking().ToListAsync();
    }

    public async Task<Area> GetAreaByIdAsync(int id)
    {
        return await _context.Areas.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AreaExistsAsync(string description)
    {
        return await _context.Areas.AsNoTracking().Where(x => x.Description == description).AnyAsync();
    }
}
