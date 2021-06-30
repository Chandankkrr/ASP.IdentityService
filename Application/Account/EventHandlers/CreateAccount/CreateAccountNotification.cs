using MediatR;

namespace Application.Account.EventHandlers.CreateAccount
{
    public class CreateAccountNotification: INotification
    {
        public string Email { get; init; }
        
        public string VerifyEmailLink { get; set; }
    }
}