using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Queries
{
    public class GetUserQueryHandler: IRequestHandler<GetUserQuery, ApplicationUserResult>
    {
        private readonly IIdentityService _identityService;
        
        private readonly ITokenService _tokenService;

        public GetUserQueryHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public Task<ApplicationUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetSidFromToken(request.Token);
            var user = _identityService.GetUserByIdAsync(userId);

            return user;
        }
    }
}