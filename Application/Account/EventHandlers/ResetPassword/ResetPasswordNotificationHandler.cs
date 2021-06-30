using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Account.EventHandlers.ResetPassword
{
    public class ResetPasswordNotificationHandler: INotificationHandler<ResetPasswordNotification>
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public ResetPasswordNotificationHandler(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task Handle(ResetPasswordNotification notification, CancellationToken cancellationToken)
        {
            var message = new EmailMessage
            {
                From = _configuration["FromEmailAddress"],
                To = notification.Email,
                Subject = "Reset password link",
                PlainTextContent = $"Hi there, please use the link to reset your password {notification.ResetPasswordLink}",
                HtmlContent = $"Hi there, please use the link to reset your password <a href=\"{notification.ResetPasswordLink}\">{notification.ResetPasswordLink}</a>"
            };
            
            await _emailService.Send(message);
        }
    }
}