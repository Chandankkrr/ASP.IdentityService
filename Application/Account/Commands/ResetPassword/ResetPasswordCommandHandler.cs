using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordCommandResult>
    {
        private readonly IIdentityService _identityService;

        public ResetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ResetPasswordCommandResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var registrationResponse = await _identityService.ResetPasswordAsync(user, request.Token, request.NewPassword);

            return registrationResponse;
        }
    }
}