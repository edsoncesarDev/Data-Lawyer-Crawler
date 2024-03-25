
using DataLawyer.Application.Processes.Commands;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using MediatR;

namespace DataLawyer.Application.Processes.Handlers;

public class ProcessCreateCommandHandler : IRequestHandler<ProcessCreateCommand, bool>
{
    private readonly IGeneralPersistence _persistence;
    private readonly IProcessCrawler _crawler;
    private readonly IProcess _process;

    public ProcessCreateCommandHandler(IGeneralPersistence persistence, IProcessCrawler crawler, IProcess process)
    {
        _persistence = persistence;
        _crawler = crawler;
        _process = process;
    }

    public async Task<bool> Handle(ProcessCreateCommand request, CancellationToken cancellationToken)
    {
        var processExists = await _process.ProcessExistsByNumberProcessAsync(request.Process);

        if(processExists)
            DomainValidationExceptions.When(true, "Existing process in the database.");

        var process = await _crawler.CrawlerProcessAsync(request.Process);

        if (process is null)
            DomainValidationExceptions.When(true, "Error when trying to register process.");

        _persistence.Add(process!);

        return await _persistence.SaveChangesAsync();

    }
}
