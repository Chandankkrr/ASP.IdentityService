namespace Application.Common.Models.Account
{
    public class ApplicationUserResult: Result
    {
        public ApplicationUser User { get; init; }
        
        public string EmailConfirmationToken { get; init; }
    }
}