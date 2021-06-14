namespace Contracts.Account.Requests
{
    public class CreateAccountRequest
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}