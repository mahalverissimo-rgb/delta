using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

            // Configuração do Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Configuração do Application Insights
            builder.Services.AddApplicationInsightsTelemetry();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<CompanySettingsService>();

            // Configuração do banco de dados
            var companySettings = builder.Configuration.GetSection("CompanySettings").Get<CompanySettings>();
            var dbName = $"{(companySettings?.ShortName ?? "Events")}Delta.db";
            var connectionString = $"Data Source={dbName}";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

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
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configuração de Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.SlidingExpiration = true;
            });

            // Configuração de autenticação e autorização
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddCookie();

            var app = builder.Build();

            // Garantir que o banco de dados seja criado
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocorreu um erro ao criar o banco de dados.");
                }
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

            // Criar o banco de dados
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                DbSeeder.SeedData(context);

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

