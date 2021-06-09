using MediatR;

namespace Application.Common.Models.Account
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}