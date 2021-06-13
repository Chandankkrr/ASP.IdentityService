using System.Threading.Tasks;
using Application.Login.Commands;
using AutoMapper;
using Contracts.Requests.Login;
using Contracts.Responses.Account;
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
        public async Task<IActionResult> Index([FromBody] LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);
            
            var result = await _mediator.Send(command);

            var response = _mapper.Map<LoginResponse>(result);

            return Ok(response);
        }

        [HttpGet("version")]
        public ActionResult<string> Version()
        {
            var version = typeof(LoginController).Assembly.GetName().Version!.ToString();
            
            return version;
        }
    }
}