using AutoMapper;
using DataLawyer.Application.Processes.Commands;
using DataLawyer.Application.Processes.Queries;
using DataLawyer.Domain.Validation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataLawyer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcessesController : ControllerBase
    {
        private readonly IMediator _mediat;
        private readonly IMapper _mapper;

        public ProcessesController(IMediator mediat, IMapper mapper)
        {
            _mediat = mediat;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTheProcesses()
        {
            var processes = await _mediat.Send(new GetAllProcessCommand());

            if(processes is null) return NoContent();

            return Ok(processes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProcess(int id)
        {
            var process = await _mediat.Send(new GetProcessByIdCommand { Id = id });

            if (process is null) return NoContent();

            return Ok(process);
        }

        [HttpPost]
        public async Task<IActionResult> AddProcess([FromBody] ProcessCreateCommand command)
        {
            var response = await _mediat.Send(command);

            if(!response) return BadRequest();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProcess([FromBody] ProcessUpdateCommand command, int id)
        {
            if (id != command.Id)
                DomainValidationExceptions.When(true, "Invalid id.");

            var response = await _mediat.Send(command);

            if (!response) return BadRequest();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            var response = await _mediat.Send(new ProcessDeleteCommand { Id = id });

            if (!response) return NoContent();

            return Ok();
        }
    }
}
