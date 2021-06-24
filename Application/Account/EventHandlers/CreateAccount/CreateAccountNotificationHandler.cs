using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Account.EventHandlers.CreateAccount
{
    public class CreateAccountNotificationHandler: INotificationHandler<CreateAccountNotification>
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public CreateAccountNotificationHandler(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task Handle(CreateAccountNotification notification, CancellationToken cancellationToken)
        {
            var message = new EmailMessage
            {
                From = _configuration["FromEmailAddress"],
                To = notification.Email,
                Subject = "New account created successfully!",
                PlainTextContent = "Hi there, your account has been created.",
                HtmlContent = "Hi there, your account has been created."
            };

            await _emailService.Send(message);
        }
    }
}