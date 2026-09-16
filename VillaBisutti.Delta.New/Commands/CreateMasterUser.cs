using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp.Commands
{
    public static class CreateMasterUser
    {
        public static async Task<int> ExecuteAsync(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Configuração dos serviços necessários
                builder.Services.AddDbContext<Data.ApplicationDbContext>();
                builder.Services.AddIdentity<Usuario, IdentityRole>()
                    .AddEntityFrameworkStores<Data.ApplicationDbContext>()
                    .AddDefaultTokenProviders();
                builder.Services.AddScoped<UserService>();

                var app = builder.Build();

                // Obtém o serviço de usuários
                using var scope = app.Services.CreateScope();
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
                    return 0;
                }
                else
                {
                    Console.WriteLine("Erro ao criar usuário master:");
                    foreach (var error in result.errors)
                    {
                        Console.WriteLine($"- {error}");
                    }
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return 1;
            }
        }
    }
}
