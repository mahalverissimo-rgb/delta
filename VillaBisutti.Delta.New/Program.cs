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
        public static async Task Main(string[] args)
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

            // Configuração do Identity (já registra o esquema de cookie da aplicação)
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

            var app = builder.Build();

            // Preparar banco de dados e seed do administrador (assíncrono, credenciais via configuração)
            await DbInitializer.InitializeAsync(app.Services, app.Configuration);

            // Comando opcional: apenas cria o usuário master e encerra
            if (args.Contains("--create-master-user"))
            {
                await Commands.CreateMasterUserCommand.ExecuteAsync(app.Services);
                return;
            }

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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            try
            {
                Log.Information("Iniciando aplicação");
                await app.RunAsync();
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
