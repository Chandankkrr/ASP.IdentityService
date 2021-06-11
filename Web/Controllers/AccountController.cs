using System.Threading.Tasks;
using Application.Account.Commands.CreateAccount;
using Application.Account.Queries;
using AutoMapper;
using Contracts.Requests.Account;
using Contracts.Responses.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

            return Ok(response);
        }
        
        [HttpGet("user")]
        public async Task<ActionResult<GetUserResponse>> GetUser(GetUserRequest request)
        {
            var query = _mapper.Map<GetUserQuery>(request);
            
            var result = await _mediator.Send(query);

            var response = _mapper.Map<GetUserResponse>(result);

            return Ok(response);
        }
    }
}
