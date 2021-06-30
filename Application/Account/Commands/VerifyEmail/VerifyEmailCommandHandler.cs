using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailCommandResult>
    {
        private readonly IIdentityService _identityService;

        public VerifyEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<VerifyEmailCommandResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var verifyEmailResponse = await _identityService.VerifyEmailAsync(request.Email, request.Token);

            return verifyEmailResponse;
        }
    }
}