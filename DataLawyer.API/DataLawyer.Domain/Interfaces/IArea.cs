
using DataLawyer.Domain.Entities;

namespace DataLawyer.Domain.Interfaces;

public interface IArea
{
    Task<IEnumerable<Area>> GetAllAreasAsync();
    Task<Area> GetAreaByIdAsync(int id);
    Task<bool> AreaExistsAsync(string description);
}
