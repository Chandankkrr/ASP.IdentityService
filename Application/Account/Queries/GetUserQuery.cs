using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Queries
{
    public class GetUserQuery: IRequest<ApplicationUserResult>
    {
        public string Token { get; set; }
    }
}