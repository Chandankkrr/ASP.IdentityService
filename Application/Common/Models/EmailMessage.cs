using Application.Common.Interfaces;

namespace Application.Common.Models
{
    public class EmailMessage : IMessage
    {
        public string From { get; set; }
        
        public string To { get; set; }
        
        public string Subject { get; set; }
        
        public string PlainTextContent { get; set; }
        
        public string HtmlContent { get; set; }
    }
}