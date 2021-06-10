namespace Contracts.Requests.Account
{
    public class CreateAccountRequest
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}