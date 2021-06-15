using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Login;
using MediatR;

namespace Application.Login.Commands
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IIdentityService _identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginResponse = await _identityService.LoginAsync(request.Email, request.Password);

            return loginResponse;
        }
    }
}