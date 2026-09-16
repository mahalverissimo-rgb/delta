using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VillaBisutti.Delta.WebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp.Commands
{
    public static class CreateMasterUserCommand
    {
        public static async Task ExecuteAsync(IServiceProvider services)
        {
            try
            {
                using var scope = services.CreateScope();
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();

                // Dados do usuário master
                var email = "admin@villabisutti.com.br";
                var password = "Admin@123456";
                var nome = "Administrador";

                // Cria o usuário master
                var result = await userService.CreateMasterUserAsync(email, password, nome);
                if (result.success)
                {
                    Console.WriteLine("Usuário master criado com sucesso!");
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Senha: {password}");
                }
                else
                {
                    Console.WriteLine("Erro ao criar usuário master:");
                    foreach (var error in result.errors)
                    {
                        Console.WriteLine($"- {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
