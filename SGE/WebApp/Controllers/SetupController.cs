using Microsoft.AspNetCore.Mvc;
using SGE.Services;

namespace SGE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public SetupController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("create-master-user")]
        public async Task<IActionResult> CreateMasterUser([FromHeader(Name = "Setup-Key")] string setupKey)
        {
            var configKey = _configuration["SetupKey"];
            if (string.IsNullOrEmpty(configKey) || setupKey != configKey)
            {
                return Unauthorized("Chave de setup inválida");
            }

            var result = await _userService.CreateMasterUserAsync(
                "admin@villabisutti.com.br",
                "Admin@123456",
                "Administrador"
            );

            if (result.success)
            {
                return Ok(new { message = "Usuário master criado com sucesso" });
            }

            return BadRequest(new { errors = result.errors });
        }
    }
}
