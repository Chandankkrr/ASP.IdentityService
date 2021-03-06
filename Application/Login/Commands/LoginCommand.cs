using Application.Common.Models.Login;
using MediatR;

namespace Application.Login.Commands
{
    public class LoginCommand: IRequest<LoginCommandResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}