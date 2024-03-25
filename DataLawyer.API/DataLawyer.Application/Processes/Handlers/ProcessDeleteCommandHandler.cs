
using AutoMapper;
using DataLawyer.Application.Processes.Commands;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using MediatR;

namespace DataLawyer.Application.Processes.Handlers;

public class ProcessDeleteCommandHandler : IRequestHandler<ProcessDeleteCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProcess _process;
    private readonly IGeneralPersistence _persistence;

    public ProcessDeleteCommandHandler(IMapper mapper, IProcess process, IGeneralPersistence persistence)
    {
        _mapper = mapper;
        _process = process;
        _persistence = persistence;
    }

    public async Task<bool> Handle(ProcessDeleteCommand request, CancellationToken cancellationToken)
    {
        var process = await _process.GetProcessByIdAsync(request.Id);

        if (process is null) return false;

        _persistence.Delete(process!);

        return await _persistence.SaveChangesAsync();
    }
}
