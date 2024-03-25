

using AutoMapper;
using DataLawyer.Application.Processes.Commands;
using DataLawyer.Domain.Entities;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Domain.Validation;
using MediatR;

namespace DataLawyer.Application.Processes.Handlers;

public class ProcessUpdateCommandHandler : IRequestHandler<ProcessUpdateCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProcess _process;
    private readonly IMovement _movement;
    private readonly IGeneralPersistence _persistence;
    private readonly IArea _area;

    public ProcessUpdateCommandHandler(IMapper mapper, IProcess process, IMovement movement, IGeneralPersistence persistence, IArea area)
    {
        _mapper = mapper;
        _process = process;
        _movement = movement;
        _persistence = persistence;
        _area = area;
    }

    public async Task<bool> Handle(ProcessUpdateCommand request, CancellationToken cancellationToken)
    {
        var processes = new List<int>();
        var process = await _process.GetProcessByIdAsync(request.Id);

        if(process is null)
            DomainValidationExceptions.When(true, "Error when trying to retrieve process.");

        var area = await _area.GetAreaByIdAsync(request.Area.Id);

        if(area is null)
            DomainValidationExceptions.When(true, "Error when trying to retrieve area.");

        area!.SetArea(request.Area.Description);

        _persistence.Update(area);

        process!.SetProcess(request.ProcessNumber, request.Situation, request.Grade, area, request.Topic, request.From, request.Distribution, request.Rapporteur);

        if(request.Movements.Count() > 0)
        {
            foreach (var item in request.Movements)
            {
                processes.Add(item.Id);

                if(item.Id == 0)
                {
                    var movementExists = await _movement.MovementExistsAsync(item.TheMovement);
                    
                    if(movementExists)
                        DomainValidationExceptions.When(true, "Existing movement.");

                    var newMovement = new Movement(item.TheMovement, item.DateMovement);

                    process!.AddMovement(newMovement);

                    _persistence.Add(newMovement);


                }
                else
                {
                    var movement = process.Movements.FirstOrDefault(x => x.Id == item.Id);

                    if (movement != null)
                    {
                        movement.SetMovement(item.TheMovement, item.DateMovement);
                        _persistence.Update(movement);
                    }
                        
                }
            }
        }

        if(process.Movements.Count() > processes.Count())
        {
            var processMovement = process.Movements.Where(x => !processes.Contains(x.Id)).ToList();

            if(processMovement.Count() > 0)
            {
                foreach (var item in processMovement)
                {
                    _persistence.Delete(item);
                }
            }
        }

        _persistence.Update(process);

       return await _persistence.SaveChangesAsync();

    }
}
