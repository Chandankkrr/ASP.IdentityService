using System;
using System.Threading.Tasks;
using Application.Common.Models.Account;
using Application.Common.Models.Login;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginResult> LoginAsync(string email, string password);

        Task<ApplicationUserResult> RegisterAsync(IdentityUser user, string password);

        Task<ApplicationUserResult> GetUserAsync(Guid userId);
    }
}