using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<VerifyEmailCommandResult>
    {
        public string Email { get; init; }

        public string Token { get; init; }
    }
}