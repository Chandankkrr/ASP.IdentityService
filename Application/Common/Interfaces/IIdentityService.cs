using System;
using System.Threading.Tasks;
using Application.Common.Models.Account;
using Application.Common.Models.Login;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginCommandResult> LoginAsync(string email, string password);

        Task<ApplicationUserResult> RegisterAsync(IdentityUser user, string password);

        Task<ApplicationUserResult> GetUserByIdAsync(Guid userId);

        Task<ChangePasswordCommandResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}