using Microsoft.AspNetCore.Identity;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Services
{
    public class UserService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<(bool success, List<string> errors)> CreateMasterUserAsync(string email, string password, string nome)
        {
            var errors = new List<string>();

            try
            {
                var masterRole = "Master";
                if (!await _roleManager.RoleExistsAsync(masterRole))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(masterRole));
                    if (!roleResult.Succeeded)
                    {
                        errors.AddRange(roleResult.Errors.Select(e => e.Description));
                        return (false, errors);
                    }
                }

                var user = new Usuario
                {
                    UserName = email,
                    Email = email,
                    Nome = nome,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, masterRole);
                    if (roleResult.Succeeded)
                    {
                        _logger.LogInformation($"Usuário master criado com sucesso: {email}");
                        return (true, errors);
                    }

                    errors.AddRange(roleResult.Errors.Select(e => e.Description));
                }
                else
                {
                    errors.AddRange(result.Errors.Select(e => e.Description));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário master");
                errors.Add("Erro interno ao criar usuário master");
            }

            return (false, errors);
        }
    }
}
