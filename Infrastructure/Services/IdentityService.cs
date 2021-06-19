using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using Application.Common.Models.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(UserManager<IdentityUser> userManager, ITokenService tokenService, ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<LoginCommandResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var userNotFoundMessage = $"User with email: {email} does not exists";

                _logger.LogWarning(userNotFoundMessage);

                return new LoginCommandResult { Errors = new[] { userNotFoundMessage } };
            }

            var isValidEmailPasswordCombination = await _userManager.CheckPasswordAsync(user, password);

            if (!isValidEmailPasswordCombination)
            {
                _logger.LogWarning("Login failed for user, email or password is incorrect");

                return new LoginCommandResult { Errors = new[] { "Email or password is incorrect" } };
            }

            var token = _tokenService.GenerateToken(user.Id);

            return new LoginCommandResult
            {
                Success = true,
                Token = token
            };
        }

        public async Task<ApplicationUserResult> RegisterAsync(string email, string password)
        {
            var userExists = await _userManager.FindByEmailAsync(email);

            if (userExists != null)
            {
                return new ApplicationUserResult
                {
                    Errors = new[] { $"User with email: {email} already exists" }
                };
            }

            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };
            
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new ApplicationUserResult
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new ApplicationUserResult
            {
                Success = true
            };
        }

        public async Task<ApplicationUserResult> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ApplicationUserResult
                {
                    Errors = new[] { $"User with id: {userId} does not exists" }
                };
            }

            return new ApplicationUserResult
            {
                User = new ApplicationUser
                {
                    Id = userId,
                    Email = user.Email,
                },
                Success = true
            };
        }

        public async Task<ChangePasswordCommandResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return new ChangePasswordCommandResult
                {
                    Errors = new[] { $"User with id: {userId} does not exists" }
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                return new ChangePasswordCommandResult
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            
            return new ChangePasswordCommandResult
            {
                Success = true
            };
        }

        public async Task<ResetPasswordCommandResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var userNotFoundMessage = $"User with email: {email} does not exists";

                _logger.LogWarning(userNotFoundMessage);

                return new ResetPasswordCommandResult { Errors = new[] { userNotFoundMessage } };
            }
            
            var decodedToken = HttpUtility.UrlDecode(token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

            if (!result.Succeeded)
            {
                return new ResetPasswordCommandResult
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new ResetPasswordCommandResult
            {
                Response = "Password reset successful",
                Success = true
            };
        }

        public async Task<ForgotPasswordResult> GetPasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var userNotFoundMessage = $"User with email: {email} does not exists";

                _logger.LogWarning(userNotFoundMessage);

                return new ForgotPasswordResult { Errors = new[] { userNotFoundMessage } };
            }

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            return new ForgotPasswordResult
            {
                PasswordResetToken = passwordResetToken,
                Success = true
            };
        }
    }
}
