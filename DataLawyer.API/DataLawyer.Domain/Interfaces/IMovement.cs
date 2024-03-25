
using DataLawyer.Domain.Entities;

namespace DataLawyer.Domain.Interfaces;

public interface IMovement
{
    Task<IEnumerable<Movement>> GetAllMovementsAsync();
    Task<Movement> GetMovementByIdAsync(int id);
    Task<bool> MovementExistsAsync(string movement);
}
