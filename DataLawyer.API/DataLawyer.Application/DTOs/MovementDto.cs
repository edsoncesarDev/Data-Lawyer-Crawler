
namespace DataLawyer.Application.DTOs;

public class MovementDto
{
    public int Id { get; set; }
    public string TheMovement { get; set; } = null!;
    public DateTime DateMovement { get; set; }
}
