namespace Contracts.Account.Requests
{
    public class VerifyEmailRequest
    {
        public string Email { get; init; }

        public string Token { get; init; }
    }
}