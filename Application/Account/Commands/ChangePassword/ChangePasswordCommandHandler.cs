using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler: IRequestHandler<ChangePasswordCommand, ChangePasswordResult>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public ChangePasswordCommandHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public Task<ChangePasswordResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var decodedToken = _tokenService.DecodeToken(request.Token);
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            var changePasswordResponse =
                _identityService.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);

            return changePasswordResponse;
        }
    }
}