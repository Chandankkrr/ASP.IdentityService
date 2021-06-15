using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.ChangePassword
{
    public class ChangePasswordCommand: IRequest<ChangePasswordCommandResult>
    {
        public string CurrentPassword { get; set; }
        
        public string NewPassword { get; set; }
        
        public string ConfirmNewPassword { get; set; }
        
        public string Token { get; set; }
    }
}