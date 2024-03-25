
using DataLawyer.Domain.Shared;

namespace DataLawyer.Domain.Entities;

public class Area : EntityBase
{
    public string Description { get; private set; } = null!;
    public int ProcessId { get; private set; }

    public Area() { }
   
    public Area(string description)
    {
        SetArea(description);
    }

    public void SetArea(string description) 
    {
        Description = description;
    }
}
