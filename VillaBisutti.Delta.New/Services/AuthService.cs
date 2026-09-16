using Microsoft.AspNetCore.Identity;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Services
{
    public class AuthService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(bool success, string[] errors)> RegisterUserAsync(string email, string password, string nome)
        {
            var user = new Usuario
            {
                UserName = email,
                Email = email,
                Nome = nome,
                Ativo = true,
                EmailConfirmed = true // Para simplificar, não vamos implementar confirmação de email agora
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return (true, Array.Empty<string>());
            }

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool success, string[] errors)> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, new[] { "Usuário não encontrado." });
            }

            if (!user.Ativo)
            {
                return (false, new[] { "Usuário inativo." });
            }

            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                user.UltimoAcesso = DateTime.Now;
                await _userManager.UpdateAsync(user);
                return (true, Array.Empty<string>());
            }

            if (result.IsLockedOut)
            {
                return (false, new[] { "Conta bloqueada temporariamente por tentativas inválidas." });
            }

            return (false, new[] { "Login ou senha inválidos." });
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var newPassword = GenerateRandomPassword();
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                // TODO: Enviar email com a nova senha
                return true;
            }

            return false;
        }

        private string GenerateRandomPassword()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
