using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler: IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResult>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public ChangePasswordCommandHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public Task<ChangePasswordCommandResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetSidFromToken(request.Token);

            var changePasswordResponse =
                _identityService.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);

            return changePasswordResponse;
        }
    }
}