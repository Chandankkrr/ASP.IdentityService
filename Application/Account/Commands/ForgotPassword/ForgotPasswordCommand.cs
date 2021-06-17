using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.ForgotPassword
{
    public class ForgotPasswordCommand: IRequest<ForgotPasswordResult>
    {
        public string Email { get; set; }
    }
}