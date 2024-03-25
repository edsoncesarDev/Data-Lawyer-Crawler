using AutoMapper;
using DataLawyer.Application.DTOs;
using DataLawyer.Application.Processes.Queries;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using MediatR;

namespace DataLawyer.Application.Processes.Handlers;

public class ProcessGetCommandHandler : IRequestHandler<GetAllProcessCommand, IEnumerable<ProcessDto>>
{
    private readonly IMapper _mapper;
    private readonly IProcess _process;

    public ProcessGetCommandHandler(IMapper mapper, IProcess process)
    {
        _mapper = mapper;
        _process = process;
    }

    public async Task<IEnumerable<ProcessDto>> Handle(GetAllProcessCommand request, CancellationToken cancellationToken)
    {
        var process = await _process.GetAllProcessAsync();

        if (process is null)
            DomainValidationExceptions.When(true, "Error when trying to retrieve processes.");

        return _mapper.Map<IEnumerable<ProcessDto>>(process);
        
    }
}
