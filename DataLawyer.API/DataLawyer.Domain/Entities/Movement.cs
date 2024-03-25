
using DataLawyer.Domain.Shared;

namespace DataLawyer.Domain.Entities;

public class Movement : EntityBase
{
    public string TheMovement { get; private set; } = null!;
    public DateTime DateMovement { get; private set; }
    public int ProcessId { get; private set; }

    public Movement() { }
    
    public Movement(string movement, DateTime dateMovement)
    {
        SetMovement(movement, dateMovement);
    }

    public void SetMovement(string movement, DateTime dateMovement)
    {
        TheMovement = movement;
        DateMovement = dateMovement;
    }
}
