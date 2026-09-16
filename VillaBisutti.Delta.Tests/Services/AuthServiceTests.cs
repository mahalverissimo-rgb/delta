using Microsoft.AspNetCore.Identity;
using Moq;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;
using Xunit;

namespace VillaBisutti.Delta.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<Usuario>> _userManagerMock;
        private readonly Mock<SignInManager<Usuario>> _signInManagerMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            var userStoreMock = new Mock<IUserStore<Usuario>>();
            _userManagerMock = new Mock<UserManager<Usuario>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<Usuario>>();
            
            _signInManagerMock = new Mock<SignInManager<Usuario>>(
                _userManagerMock.Object,
                contextAccessorMock.Object,
                userPrincipalFactoryMock.Object,
                null, null, null, null);

            _authService = new AuthService(_userManagerMock.Object, _signInManagerMock.Object);
        }

        [Fact]
        public async Task RegisterUser_Success()
        {
            // Arrange
            var email = "test@test.com";
            var password = "Test@123";
            var nome = "Test User";

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RegisterUserAsync(email, password, nome);

            // Assert
            Assert.True(result.success);
            Assert.Empty(result.errors);
            _userManagerMock.Verify(x => x.CreateAsync(It.Is<Usuario>(u => 
                u.Email == email && 
                u.Nome == nome && 
                u.UserName == email), 
                password), Times.Once);
        }

        [Fact]
        public async Task RegisterUser_Failure()
        {
            // Arrange
            var email = "test@test.com";
            var password = "Test@123";
            var nome = "Test User";
            var errors = new[] { new IdentityError { Description = "Error" } };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), password))
                .ReturnsAsync(IdentityResult.Failed(errors));

            // Act
            var result = await _authService.RegisterUserAsync(email, password, nome);

            // Assert
            Assert.False(result.success);
            Assert.Single(result.errors);
            Assert.Equal("Error", result.errors[0]);
        }

        [Fact]
        public async Task Login_Success()
        {
            // Arrange
            var email = "test@test.com";
            var password = "Test@123";
            var user = new Usuario { Email = email, Ativo = true };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(email, password, true, true))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _authService.LoginAsync(email, password, true);

            // Assert
            Assert.True(result.success);
            Assert.Empty(result.errors);
        }

        [Fact]
        public async Task Login_UserNotFound()
        {
            // Arrange
            var email = "test@test.com";
            var password = "Test@123";

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync((Usuario)null);

            // Act
            var result = await _authService.LoginAsync(email, password, true);

            // Assert
            Assert.False(result.success);
            Assert.Single(result.errors);
            Assert.Equal("Usuário não encontrado.", result.errors[0]);
        }

        [Fact]
        public async Task Login_UserInactive()
        {
            // Arrange
            var email = "test@test.com";
            var password = "Test@123";
            var user = new Usuario { Email = email, Ativo = false };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(email, password, true);

            // Assert
            Assert.False(result.success);
            Assert.Single(result.errors);
            Assert.Equal("Usuário inativo.", result.errors[0]);
        }

        [Fact]
        public async Task Login_InvalidPassword()
        {
            // Arrange
            var email = "test@test.com";
            var password = "Test@123";
            var user = new Usuario { Email = email, Ativo = true };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(email, password, true, true))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _authService.LoginAsync(email, password, true);

            // Assert
            Assert.False(result.success);
            Assert.Single(result.errors);
            Assert.Equal("Login ou senha inválidos.", result.errors[0]);
        }

        [Fact]
        public async Task ChangePassword_Success()
        {
            // Arrange
            var userId = "1";
            var currentPassword = "Current@123";
            var newPassword = "New@123";
            var user = new Usuario { Id = 1 };

            _userManagerMock.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);

            _userManagerMock.Setup(x => x.ChangePasswordAsync(user, currentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ChangePassword_UserNotFound()
        {
            // Arrange
            var userId = "1";
            var currentPassword = "Current@123";
            var newPassword = "New@123";

            _userManagerMock.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync((Usuario)null);

            // Act
            var result = await _authService.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            Assert.False(result);
        }
    }
}
