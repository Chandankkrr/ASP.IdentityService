using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services
{
    public class SendgridEmailService: IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendgridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Send(IMessage message)
        {
            var apiKey = _configuration["Sendgrid:ApiKey"];
            
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(message.From, "Support Team"),
                Subject = message.Subject,
                PlainTextContent = message.PlainTextContent,
                HtmlContent = message.HtmlContent
            };
            
            msg.AddTo(new EmailAddress(message.To));
            msg.AddBcc(_configuration["BccEmailAddress"]);
            
            var response = await client.SendEmailAsync(msg);

            return "Success";
        }
    }
}