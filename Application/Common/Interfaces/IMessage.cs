namespace Application.Common.Interfaces
{
    public interface IMessage
    {
        public string From { get; set; }
        
        public string To { get; set; }

        public string Subject { get; set; }
        
        public string PlainTextContent { get; set; }
        
        public string HtmlContent { get; set; }
    }
}