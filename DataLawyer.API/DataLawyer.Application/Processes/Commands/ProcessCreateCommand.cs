

using DataLawyer.Domain.Entities;
using MediatR;

namespace DataLawyer.Application.Processes.Commands;

public class ProcessCreateCommand : IRequest<bool>
{
    public string Process { get; set; } = null!;
}
