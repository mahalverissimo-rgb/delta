using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Data
{
    /// <summary>
    /// Prepara o banco (schema + seed do administrador) de forma assíncrona no startup.
    /// As credenciais do admin vêm da configuração (seção "AdminUser"), nunca do código.
    /// </summary>
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services, IConfiguration configuration)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var context = provider.GetRequiredService<ApplicationDbContext>();
            await EnsureSchemaAsync(context);

            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<Usuario>>();
            await SeedAdminAsync(configuration, roleManager, userManager);
        }

        private static async Task EnsureSchemaAsync(ApplicationDbContext context)
        {
            // Assim que migrations forem geradas (dotnet ef migrations add), elas passam a ser aplicadas.
            // Enquanto não existirem, mantemos EnsureCreated para não quebrar a criação do schema.
            var hasMigrations = context.Database.GetMigrations().Any();
            if (hasMigrations)
            {
                await context.Database.MigrateAsync();
            }
            else
            {
                await context.Database.EnsureCreatedAsync();
            }
        }

        private static async Task SeedAdminAsync(
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            UserManager<Usuario> userManager)
        {
            var email = configuration["AdminUser:Email"];
            var password = configuration["AdminUser:Password"];
            var nome = configuration["AdminUser:Nome"] ?? "Administrador";

            // Sem credenciais configuradas não criamos admin (evita credenciais fixas no código).
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new Usuario
                {
                    UserName = email,
                    Email = email,
                    Nome = nome,
                    Ativo = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
