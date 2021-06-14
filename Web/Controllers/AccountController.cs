using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Account.Commands.ChangePassword;
using Application.Account.Commands.CreateAccount;
using Application.Account.Queries;
using AutoMapper;
using Contracts.Account.Requests;
using Contracts.Account.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediatR, IMapper mapper)
        {
            _mediator = mediatR;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateAccountResponse>> Create(CreateAccountRequest request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);
            
            var result = await _mediator.Send(command);

            var response = _mapper.Map<CreateAccountResponse>(result);

            return CreatedAtAction("GetUser", response);
        }
        
        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<GetUserResponse>> GetUser([FromQuery] Guid id)
        {
            var request = new GetUserRequest
            {
                Id = id
            };
            var query = _mapper.Map<GetUserQuery>(request);
            
            var result = await _mediator.Send(query);

            var response = _mapper.Map<GetUserResponse>(result);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<ActionResult<ChangePasswordResponse>> ChangePassword(ChangePasswordRequest request)
        {
            var authorizationHeader = Request.Headers[HeaderNames.Authorization];
            AuthenticationHeaderValue.TryParse(authorizationHeader, out var bearerToken);
            
            var command = _mapper.Map<ChangePasswordCommand>(request);
            command.Token = bearerToken?.Parameter;

            var result = await _mediator.Send(command);

            var response = _mapper.Map<ChangePasswordResponse>(result);

            return Ok(response);
        }
    }
}
