using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        public Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}