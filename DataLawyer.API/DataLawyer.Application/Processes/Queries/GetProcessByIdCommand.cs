

using DataLawyer.Application.DTOs;
using MediatR;

namespace DataLawyer.Application.Processes.Queries;

public class GetProcessByIdCommand : IRequest<ProcessDto>
{
    public int Id { get; set; }
}
