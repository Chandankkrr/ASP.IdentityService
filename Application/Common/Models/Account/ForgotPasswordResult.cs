namespace Application.Common.Models.Account
{
    public class ForgotPasswordResult: Result
    {
        public string PasswordResetToken { get; set; }
    }
}