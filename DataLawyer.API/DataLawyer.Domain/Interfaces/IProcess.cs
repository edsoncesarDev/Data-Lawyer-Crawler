
using DataLawyer.Domain.Entities;

namespace DataLawyer.Domain.Interfaces;

public interface IProcess
{
    Task<IEnumerable<Process>> GetAllProcessAsync();
    Task<Process> GetProcessByIdAsync(int id);
    Task<bool> ProcessExistsByNumberProcessAsync(string processNumber);

}
