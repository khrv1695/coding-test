using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<IdentityUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public class RegisterUserDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Registering user: {Username}", model.Username);
                var user = new IdentityUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {Username} registered successfully", model.Username);
                    return Ok(new { Message = "User registered successfully" });
                }

                _logger.LogError("User registration failed for {Username}. Errors: {Errors}", model.Username, result.Errors);
                return BadRequest(new { Errors = result.Errors });
            }

            _logger.LogWarning("Invalid registration attempt. Model state errors: {ModelState}", ModelState);
            return BadRequest(ModelState);
        }
    }
}
