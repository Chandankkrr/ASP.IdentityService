using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordCommandResult>
    {
        public string Email { get; init; }
        
        public string Token { get; init; }
        
        public string NewPassword { get; init; }

        public string ConfirmNewPassword { get; init; }
    }
}