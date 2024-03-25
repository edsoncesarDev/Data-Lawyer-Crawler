using AutoMapper;
using DataLawyer.Application.DTOs;
using DataLawyer.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataLawyer.API.Controllers
{
    [AllowAnonymous]
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost, Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(_mapper.Map<UserDto>(response));
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginCommand command)
        {
            var response = await _mediator.Send(command);

            if (response is null)
                return BadRequest();

            return Ok(response);
        }

    }
}
