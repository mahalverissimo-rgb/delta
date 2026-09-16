using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.ViewModels;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Route("setup")]
    public class InitialSetupController : Controller
    {
        private void CopyDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var fileName = Path.GetFileName(file);
                var destFile = Path.Combine(targetDir, fileName);
                File.Copy(file, destFile, true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                var dirName = Path.GetFileName(directory);
                var destDir = Path.Combine(targetDir, dirName);
                CopyDirectory(directory, destDir);
            }
        }

        private readonly IConfiguration _configuration;
        private readonly string _configPath;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitialSetupController(IConfiguration configuration, IWebHostEnvironment env, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _configPath = Path.Combine(env.ContentRootPath, "appsettings.json");
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var settings = _configuration.GetSection("CompanySettings").Get<CompanySettings>();
            var model = new InitialSetupViewModel
            {
                CompanySettings = settings ?? new CompanySettings
                {
                    Name = "SGE - Sistema de Gerenciamento de Eventos",
                    ShortName = "SGE",
                    Logo = "/img/logo.png",
                    Theme = "default"
                },
                AdminUser = new RegisterViewModel()
            };
            return View(model);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(InitialSetupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                // Processar o upload do logo
                if (model.LogoFile != null && model.LogoFile.Length > 0)
                {
                    // Validar tamanho do arquivo (2MB)
                    if (model.LogoFile.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("LogoFile", "O arquivo deve ter no máximo 2MB");
                        return View("Index", model);
                    }

                    // Validar tipo do arquivo
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(model.LogoFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("LogoFile", "Apenas arquivos JPG, PNG e GIF são permitidos");
                        return View("Index", model);
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "logos");
                    Directory.CreateDirectory(uploadsFolder); // Criar pasta se não existir

                    // Remover arquivo antigo se existir
                    if (!string.IsNullOrEmpty(model.CompanySettings.Logo))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", 
                            model.CompanySettings.Logo.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.LogoFile.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.LogoFile.CopyToAsync(fileStream);
                    }

                    // Atualizar o caminho do logo nas configurações
                    model.CompanySettings.Logo = $"/uploads/logos/{uniqueFileName}";
                }

                // Criar roles se não existirem
                var roles = new[] { "Administrador", "Gerente", "Vendedor", "Operador" };
                foreach (var roleName in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Criar usuário administrador
                var user = new Usuario
                {
                    UserName = model.AdminUser.Email,
                    Email = model.AdminUser.Email,
                    Nome = model.AdminUser.Nome,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.AdminUser.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Index", model);
                }

                await _userManager.AddToRoleAsync(user, "Administrador");

                // Criar pasta do cliente
                var clientSlug = model.CompanySettings.ShortName.ToLower().Replace(" ", "-");
                var clientsFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", "CLIENTES", clientSlug);
                Directory.CreateDirectory(clientsFolder);

                // Copiar arquivos necessários
                var sourceFolder = Path.Combine(Directory.GetCurrentDirectory());
                var targetFolder = Path.Combine(clientsFolder, "WebApp");
                Directory.CreateDirectory(targetFolder);

                // Copiar apenas os arquivos necessários
                var filesToCopy = new[]
                {
                    "Program.cs",
                    "appsettings.json",
                    "VillaBisutti.Delta.WebApp.csproj"
                };

                foreach (var file in filesToCopy)
                {
                    var sourcePath = Path.Combine(sourceFolder, file);
                    var targetPath = Path.Combine(targetFolder, file);
                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, targetPath, true);
                    }
                }

                // Copiar pastas necessárias
                var foldersToCopy = new[]
                {
                    "Controllers",
                    "Models",
                    "ViewModels",
                    "Views",
                    "wwwroot"
                };

                foreach (var folder in foldersToCopy)
                {
                    var sourceDir = Path.Combine(sourceFolder, folder);
                    var targetDir = Path.Combine(targetFolder, folder);
                    if (Directory.Exists(sourceDir))
                    {
                        CopyDirectory(sourceDir, targetDir);
                    }
                }

                // Atualizar configurações
                var jsonString = System.IO.File.ReadAllText(_configPath);
                var jsonObj = JsonNode.Parse(jsonString);

                if (jsonObj is JsonObject root)
                {
                    root["CompanySettings"] = JsonNode.Parse(JsonSerializer.Serialize(model.CompanySettings));
                    
                    // Salvar no diretório do cliente
                    var clientConfigPath = Path.Combine(targetFolder, "appsettings.json");
                    System.IO.File.WriteAllText(clientConfigPath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
                    
                    // Salvar também no diretório atual
                    System.IO.File.WriteAllText(_configPath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
                }

                TempData["Message"] = "Configurações salvas com sucesso!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar as configurações: " + ex.Message);
                return View("Index", model);
            }

        }
    }
}
