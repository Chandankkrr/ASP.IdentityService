using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Account.EventHandlers.VerifyEmail
{
    public class VerifyEmailNotificationHandler: INotificationHandler<VerifyEmailNotification>
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public VerifyEmailNotificationHandler(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task Handle(VerifyEmailNotification notification, CancellationToken cancellationToken)
        {
            var message = new EmailMessage
            {
                From = _configuration["FromEmailAddress"],
                To = notification.Email,
                Subject = "New account created successfully!",
                PlainTextContent = $"Hi there, your account has been created. Please use the link to verify your email {notification.VerifyEmailLink}",
                HtmlContent = $"Hi there, your account has been created. Please use the link to verify your email <a href=\"{notification.VerifyEmailLink}\">{notification.VerifyEmailLink}</a>"
            };

            await _emailService.Send(message);
        }
    }
}