using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

namespace VillaBisutti.Delta.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Configuração do Application Insights
            builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
            {
                ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"],
                EnableAdaptiveSampling = true,
                EnableQuickPulseMetricStream = true
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configuração do banco de dados
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configuração do Identity
            builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                // Configurações de senha
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                // Configurações de bloqueio
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Configurações de usuário
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configuração de Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.SlidingExpiration = true;
            });

            // Registrar serviços
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<LogService>();
            builder.Services.AddScoped<UserService>();

            // Configuração de autenticação e autorização
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddCookie();

            var app = builder.Build();

            // Configure o pipeline de requisição HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSerilogRequestLogging(); // Adiciona logging para todas as requisições HTTP

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            // Criar o banco de dados
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                // Criar usuário master se não existir
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = new IdentityRole("Admin");
                    roleManager.CreateAsync(role).Wait();
                }

                if (userManager.FindByEmailAsync("admin@villabisutti.com.br").Result == null)
                {
                    var user = new Usuario
                    {
                        UserName = "admin@villabisutti.com.br",
                        Email = "admin@villabisutti.com.br",
                        Nome = "Administrador",
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(user, "Admin@123456").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
            }

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            try
            {
                Log.Information("Iniciando aplicação");

                // Se o argumento --create-master-user for passado, cria o usuário master
                if (args.Contains("--create-master-user"))
                {
                    await Commands.CreateMasterUserCommand.ExecuteAsync(app.Services);
                    return;
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Aplicação terminou inesperadamente");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

