using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler: IRequestHandler<ForgotPasswordCommand, ForgotPasswordResult>
    {
        private readonly IIdentityService _identityService;

        public ForgotPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ForgotPasswordResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var passwordResetToken = await _identityService.GetPasswordResetTokenAsync(request.Email);

            return passwordResetToken;
        }
    }
}