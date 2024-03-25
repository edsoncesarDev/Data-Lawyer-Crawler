
using AutoMapper;
using DataLawyer.Application.DTOs;
using DataLawyer.Application.Processes.Queries;
using DataLawyer.Domain.Interfaces;
using MediatR;

namespace DataLawyer.Application.Processes.Handlers;

public class ProcessGetByIdCommandHandler : IRequestHandler<GetProcessByIdCommand, ProcessDto>
{
    private readonly IMapper _mapper;
    private readonly IProcess _process;

    public ProcessGetByIdCommandHandler(IMapper mapper, IProcess process)
    {
        _mapper = mapper;
        _process = process;
    }

    public async Task<ProcessDto> Handle(GetProcessByIdCommand request, CancellationToken cancellationToken)
    {
        var process = await _process.GetProcessByIdAsync(request.Id);

        if (process is null) return null!;

        return _mapper.Map<ProcessDto>(process);
    }
}
