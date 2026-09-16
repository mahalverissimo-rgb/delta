using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SGE.Models;

namespace SGE.Services
{
    public class ExternalAuthenticationService
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public ExternalAuthenticationService(
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Usuario> HandleExternalLoginAsync()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new InvalidOperationException("Error loading external login information.");
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false,
                bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            }

            var email = info.Principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var nome = info.Principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
                user = new Usuario
                {
                    UserName = email,
                    Email = email,
                    Nome = nome,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var addLoginResult = await _userManager.AddLoginAsync(user, info);
            if (!addLoginResult.Succeeded)
            {
                throw new InvalidOperationException($"Error adding external login for user: {string.Join(", ", addLoginResult.Errors.Select(e => e.Description))}");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return user;
        }
    }
}
