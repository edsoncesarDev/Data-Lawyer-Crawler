using MediatR;

namespace DataLawyer.Application.Processes.Commands;

public class ProcessDeleteCommand : IRequest<bool>
{
    public int Id { get; set; }
}
