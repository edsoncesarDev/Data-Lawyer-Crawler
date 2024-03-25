using DataLawyer.Application.DTOs;
using MediatR;

namespace DataLawyer.Application.Processes.Queries;

public class GetAllProcessCommand : IRequest<IEnumerable<ProcessDto>>
{

}
