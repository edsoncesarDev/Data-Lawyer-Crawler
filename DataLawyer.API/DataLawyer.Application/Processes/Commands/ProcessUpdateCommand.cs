
using DataLawyer.Application.DTOs;
using MediatR;

namespace DataLawyer.Application.Processes.Commands;

public class ProcessUpdateCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string ProcessNumber { get; set; } = null!;
    public string Situation { get; set; } = null!;
    public string Grade { get; set; } = null!;
    public AreaDto Area { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public string From { get; set; } = null!;
    public string Distribution { get; set; } = null!;
    public string Rapporteur { get; set; } = null!;
    public List<MovementDto> Movements { get; set; } = new List<MovementDto>();
}
