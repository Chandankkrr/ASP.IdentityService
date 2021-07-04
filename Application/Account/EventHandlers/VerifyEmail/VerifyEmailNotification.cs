using MediatR;

namespace Application.Account.EventHandlers.VerifyEmail
{
    public class VerifyEmailNotification: INotification
    {
        public string Email { get; init; }
        
        public string VerifyEmailLink { get; set; }
    }
}