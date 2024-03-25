

using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Infrastructure.Repository;

public class ProcessPersistence : IProcess
{
    private readonly AppDbContext _context;

    public ProcessPersistence(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Process>> GetAllProcessAsync()
    {
        return await _context.Processes.AsNoTracking()
                                       .Include(x => x.Area)
                                       .Include(x => x.Movements)
                                       .ToListAsync();
    }

    public async Task<Process> GetProcessByIdAsync(int id)
    {
        return await _context.Processes
                            .Include(x => x.Area)
                            .Include(x => x.Movements)
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ProcessExistsByNumberProcessAsync(string processNumber)
    {
        return await _context.Processes.AsNoTracking().Where(x => x.ProcessNumber == processNumber).AnyAsync();
                                       
    }
}
