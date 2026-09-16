using Microsoft.AspNetCore.Identity;
using SGE.Models;
using SGE.ViewModels;

namespace SGE.Services
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

        public async Task<bool> CreateInitialAdminUser(RegisterViewModel model)
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
                return true;
            }

            return false;
        }
        
        public async Task<(bool success, List<string> errors)> CreateMasterUserAsync(string email, string password, string nome)
        {
            var errors = new List<string>();
            
            // Verificar se o usuário já existe
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return (true, errors); // Usuário já existe, consideramos sucesso
            }
            
            // Garantir que o papel de Administrador existe
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }
            
            // Criar o usuário
            var user = new Usuario
            {
                UserName = email,
                Email = email,
                Nome = nome,
                EmailConfirmed = true
            };
            
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return (false, errors);
            }
            
            // Adicionar ao papel de Administrador
            var roleResult = await _userManager.AddToRoleAsync(user, "Administrador");
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return (false, errors);
            }
            
            return (true, errors);
        }
    }
}
