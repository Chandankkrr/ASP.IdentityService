using MediatR;

namespace Application.Account.EventHandlers.ResetPassword
{
    public class ResetPasswordNotification : INotification
    {
        public string Email { get; init; }
        
        public string ResetPasswordLink { get; init; }
    }
}