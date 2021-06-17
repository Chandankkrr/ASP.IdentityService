using MediatR;

namespace Application.Account.EventHandlers.ResetPassword
{
    public class ResetPasswordNotification : INotification
    {
        public string Email { get; set; }
        
        public string ResetPasswordLink { get; set; }
    }
}