using Microsoft.AspNetCore.Identity;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Services
{
    public class UserService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<(bool success, string[] errors)> CreateMasterUserAsync(string email, string password, string nome)
        {
            // Verifica se o papel de Master existe
            if (!await _roleManager.RoleExistsAsync("Master"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Master"));
            }

            // Verifica se já existe um usuário com este email
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return (false, new[] { "Já existe um usuário com este email." });
            }

            // Cria o usuário master
            var user = new Usuario
            {
                UserName = email,
                Email = email,
                Nome = nome,
                Ativo = true,
                EmailConfirmed = true // Como é o master, já confirmamos o email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            // Adiciona o usuário ao papel de Master
            result = await _userManager.AddToRoleAsync(user, "Master");
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            return (true, Array.Empty<string>());
        }
    }
}
