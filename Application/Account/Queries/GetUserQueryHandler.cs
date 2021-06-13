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

        public GetUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<ApplicationUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = _identityService.GetUserByIdAsync(request.Id);

            return user;
        }
    }
}