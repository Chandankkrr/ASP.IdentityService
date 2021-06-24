using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Account.Commands.ChangePassword;
using Application.Account.Commands.CreateAccount;
using Application.Account.Commands.ForgotPassword;
using Application.Account.Commands.ResetPassword;
using Application.Account.EventHandlers.CreateAccount;
using Application.Account.EventHandlers.ResetPassword;
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
            
            if (!response.Success)
            {
                // TODO return proper status code
                return BadRequest(response);
            }
            
            var createAccountNotification = new CreateAccountNotification
            {
                Email = request.Email
            };

            await _mediator.Publish(createAccountNotification);

            return CreatedAtAction("GetUser", response);
        }
        
        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<GetUserResponse>> GetUser()
        {
            var authorizationHeader = Request.Headers[HeaderNames.Authorization];
            AuthenticationHeaderValue.TryParse(authorizationHeader, out var bearerToken);
            
            var request = new GetUserRequest
            {
                Token = bearerToken?.Parameter
            };
            
            var query = _mapper.Map<GetUserQuery>(request);
            
            var result = await _mediator.Send(query);

            var response = _mapper.Map<GetUserResponse>(result);
            
            if (!response.Success)
            {
                // TODO return proper status code
                return BadRequest(response);
            }

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
            
            if (!response.Success)
            {
                // TODO return proper status code
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("forgotpassword")]
        public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequest request)
        {
            var command = _mapper.Map<ForgotPasswordCommand>(request);

            var result = await _mediator.Send(command);
            var resetPasswordLink = Url.Action(nameof(ResetPassword), "Account", new
                {
                    email = request.Email,
                    token = result.PasswordResetToken
                },
                Request.Scheme
            );

            var resetPasswordNotification = new ResetPasswordNotification
            {
                Email = request.Email,
                ResetPasswordLink = resetPasswordLink
            };

            await _mediator.Publish(resetPasswordNotification);

            var response = new ForgotPasswordResponse
            {
                Response = "If you have an account with us, a password reset email will be sent to your account"
            };

            return Ok(response);
        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var command = _mapper.Map<ResetPasswordCommand>(request);
            
            var result = await _mediator.Send(command);

            var response = _mapper.Map<ResetPasswordResponse>(result);
            
            if (!response.Success)
            {
                // TODO return proper status code
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
