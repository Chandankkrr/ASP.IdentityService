using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<ApplicationUserResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
