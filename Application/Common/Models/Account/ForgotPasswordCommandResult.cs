namespace Application.Common.Models.Account
{
    public class ForgotPasswordCommandResult: Result
    {
        public string PasswordResetToken { get; set; }
    }
}