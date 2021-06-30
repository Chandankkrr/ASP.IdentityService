using System.Threading.Tasks;
using Application.Common.Models.Account;
using Application.Common.Models.Login;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginCommandResult> LoginAsync(string email, string password);

        Task<ApplicationUserResult> RegisterAsync(string email, string password);

        Task<ApplicationUserResult> GetUserByIdAsync(string userId);

        Task<ChangePasswordCommandResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<ResetPasswordCommandResult> ResetPasswordAsync(string email, string token, string newPassword);

        Task<ForgotPasswordCommandResult> GetPasswordResetTokenAsync(string email);

        Task<VerifyEmailCommandResult> VerifyEmailAsync(string email, string token);
    }
}