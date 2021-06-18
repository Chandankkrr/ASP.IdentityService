using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Account.EventHandlers.CreateAccount
{
    public class CreateAccountNotificationHandler: INotificationHandler<CreateAccountNotification>
    {
        private readonly IEmailService _emailService;

        public CreateAccountNotificationHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(CreateAccountNotification notification, CancellationToken cancellationToken)
        {
            var message = new EmailMessage
            {
                From = "chandankkrr@gmail.com",
                To = notification.Email,
                Subject = "New account created successfully",
                PlainTextContent = "Hi there, thanks for creating a new account with us",
                HtmlContent = "Hi there, thanks for creating a new account with us"
            };

            await _emailService.Send(message);
        }
    }
}