
using DataLawyer.Domain.Interfaces;
using DataLawyer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Infrastructure.Repository;

public class GeneralPersistence : IGeneralPersistence
{
    private readonly AppDbContext _context;

    public GeneralPersistence(AppDbContext context)
    {
        _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
