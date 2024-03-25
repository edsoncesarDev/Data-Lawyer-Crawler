
namespace DataLawyer.Domain.Shared;

public abstract class EntityBase
{
    public int Id { get; protected set; }
    public DateTime CreateAt { get; private set; } = DateTime.Now;
}
