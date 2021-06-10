using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ApplicationUserResult>
    {
        private readonly IIdentityService _identityService;

        public CreateAccountCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ApplicationUserResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var registrationResponse = await _identityService.RegisterAsync(user, request.Password);

            return registrationResponse;
        }
    }
}