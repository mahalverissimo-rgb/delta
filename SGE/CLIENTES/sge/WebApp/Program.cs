using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SGE.Data;
using SGE.Models;
using SGE.Services;

var builder = WebApplication.CreateBuilder(args);

            // Configuração do Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Serviços básicos
            builder.Services.AddScoped<CompanySettingsService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<DatabaseEncryptionService>();
            builder.Services.AddScoped<ExternalAuthenticationService>();

            // Configuração do banco de dados
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=sge.db";
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            // Configuração do Identity
            builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                // Configurações simplificadas para facilitar testes
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configuração de Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(12);
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure o pipeline de requisição HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Criar o banco de dados e dados iniciais
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                    // Garantir que o banco de dados seja criado
                    context.Database.EnsureCreated();
                    
                    // Criar papel de administrador se não existir
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!roleManager.RoleExistsAsync("Admin").Result)
                    {
                        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                    }

                    // Criar usuário administrador para teste se não existir
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
                    if (userManager.FindByEmailAsync("admin@teste.com").Result == null)
                    {
                        var user = new Usuario
                        {
                            UserName = "admin@teste.com",
                            Email = "admin@teste.com",
                            Nome = "Administrador",
                            EmailConfirmed = true
                        };

                        var result = userManager.CreateAsync(user, "1234").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Admin").Wait();
                        }
                    }
                }
                
                Console.WriteLine("Banco de dados inicializado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inicializar o banco de dados: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Iniciar o servidor
            app.Run();

