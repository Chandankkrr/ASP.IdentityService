using System.Linq;
using System.Threading.Tasks;
using Application.Login.Commands;
using AutoMapper;
using Contracts.Login.Requests;
using Contracts.Login.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoginController(IMediator mediatR, IMapper mapper)
        {
            _mediator = mediatR;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Index([FromBody] LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);
            
            var result = await _mediator.Send(command);

            var response = _mapper.Map<LoginResponse>(result);

            if (!response.Success)
            {
                // TODO return proper status code
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}