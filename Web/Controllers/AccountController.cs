using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Account.Queries;
using Application.Common.Models.Account;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateAccountResponse>> Register(CreateAccountRequest request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);
            
            var result = await _mediatR.Send(command);
            
            var response = new CreateAccountResponse
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Success = result
            };

            return Ok(response);
        }
    }
}
