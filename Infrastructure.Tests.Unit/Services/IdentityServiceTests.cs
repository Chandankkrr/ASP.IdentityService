using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using FluentAssertions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Unit.Services
{
    public class IdentityServiceTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<ILogger<IdentityService>> _loggerMock;

        private readonly IdentityService _identityService;

        public IdentityServiceTests()
        {
            _userManagerMock = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object, null,
                null, null, null, null, null, null, null);
            _tokenServiceMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger<IdentityService>>();
            _identityService =
                new IdentityService(_userManagerMock.Object, _tokenServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ReturnsUserNotFound_WhenUserWithGivenEmailDoesNotExist()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser) null);

            // Act
            var result = await _identityService.LoginAsync("test@test.com", It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == "User with email: test@test.com does not exists");
        }

        [Fact]
        public async Task LoginAsync_ReturnsIncorrectEmailOrPassword_WhenGivenEmailAndPasswordCombinationIsIncorrect()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser());
            _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _identityService.LoginAsync("test@test.com", It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == "Email or password is incorrect");
        }

        [Fact]
        public async Task LoginAsync_CanSuccessfullyLogin_WhenGivenEmailAndPasswordCombinationsAreValid()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser());
            _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<string>())).Returns("test-token");

            // Act
            var result = await _identityService.LoginAsync("test@test.com", It.IsAny<string>());

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
            result.Token.Should().NotBeEmpty();
        }

        [Fact]
        public async Task RegisterAsync_ReturnsUserAlreadyExists_WhenGivenUserAlreadyExists()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _identityService.RegisterAsync(email, It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == $"User with email: {email} already exists");
        }

        [Fact]
        public async Task RegisterAsync_FailsToRegisterUser_WhenExceptionOccurs()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser) null);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError {Description = "Failed to create user"}));

            // Act
            var result = await _identityService.RegisterAsync(email, It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == $"Failed to create user");
        }

        [Fact]
        public async Task RegisterAsync_CanSuccessfullyRegisterNewUser_WhenGivenUserAndPasswordAreValid()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser) null);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _identityService.RegisterAsync(email, It.IsAny<string>());

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUserNotFound_WhenUserWithGivenIdDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync((IdentityUser) null);

            // Act
            var result = await _identityService.GetUserByIdAsync(userId.ToString());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == $"User with id: {userId.ToString()} does not exists");
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserWithGivenIdExists()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser {Email = "test@test.com"});

            // Act
            var result = await _identityService.GetUserByIdAsync(It.IsAny<string>());

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsUserNotFound_WhenUserWithGivenIdDoesNotExist()
        {
            // Arrange
            const string userId = "test-user-id";
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser) null);

            // Act
            var result = await _identityService.ChangePasswordAsync(userId, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == $"User with id: {userId} does not exists");
        }

        [Fact]
        public async Task ChangePasswordAsync_FailsToChangePassword_WhenExceptionOccurs()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            const string userId = "test-user-id";
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock
                .Setup(x => x.ChangePasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError {Description = "Failed to change password"}));

            // Act
            var result = await _identityService.ChangePasswordAsync(userId, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == "Failed to change password");
        }

        [Fact]
        public async Task ChangePasswordAsync_CanChangePasswordSuccessfully_WhenGivenCurrentAndNewPasswordsAreValid()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            const string userId = "test-user-id";
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(x =>
                    x.ChangePasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _identityService.ChangePasswordAsync(userId, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Fact]
        public async Task ResetPasswordAsync_ReturnsUserNotFound_WhenUserWithGivenEmailDoesNotExist()
        {
            // Arrange
            const string email = "test@test.com";
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((IdentityUser) null);

            // Act
            var result = await _identityService.ResetPasswordAsync(email, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == $"User with email: {email} does not exists");
        }

        [Fact]
        public async Task ResetPasswordAsync_FailsToResetPassword_WhenExceptionOccurs()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.ResetPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed
            (
                new IdentityError {Description = "Failed to reset password"}
            ));

            // Act
            var result = await _identityService.ResetPasswordAsync(email, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == "Failed to reset password");
        }
        
        [Fact]
        public async Task ResetPasswordAsync_CanResetPasswordSuccessfully_WhenGivenEmailTokenAndNewPasswordAreValid()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.ResetPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _identityService.ResetPasswordAsync(email, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
            result.Response.Should().Be("Password reset successful");
        }

        [Fact]
        public async Task GetPasswordResetTokenAsync_ReturnsUserNotFound_WhenUserWithGivenEmailDoesNotExist()
        {
            // Arrange
            const string email = "test@test.com";
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((IdentityUser) null);

            // Act
            var result = await _identityService.GetPasswordResetTokenAsync(email);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e == $"User with email: {email} does not exists");
        }
        
        [Fact]
        public async Task GetPasswordResetTokenAsync_ReturnsPasswordResetTokenForTheUser_WhenUserWithGivenEmailExists()
        {
            // Arrange
            const string email = "test@test.com";
            var user = new IdentityUser {Email = email};
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync("test-password-reset-token");

            // Act
            var result = await _identityService.GetPasswordResetTokenAsync(email);

            // Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
            result.PasswordResetToken.Should().NotBeEmpty();
        }
    }
}