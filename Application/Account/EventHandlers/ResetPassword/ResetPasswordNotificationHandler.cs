using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Account.EventHandlers.ResetPassword
{
    public class ResetPasswordNotificationHandler: INotificationHandler<ResetPasswordNotification>
    {
        private readonly IEmailService _emailService;

        public ResetPasswordNotificationHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(ResetPasswordNotification notification, CancellationToken cancellationToken)
        {
            var message = new EmailMessage
            {
                From = "chandankkrr@gmail.com",
                To = notification.Email,
                Subject = "Reset password link",
                PlainTextContent = $"Hi there, please use the link to reset your password {notification.ResetPasswordLink}",
                HtmlContent = $"Hi there, please use the link to reset your password <a href=\"{notification.ResetPasswordLink}\">{notification.ResetPasswordLink}</a>"
            };
            
            await _emailService.Send(message);
        }
    }
}