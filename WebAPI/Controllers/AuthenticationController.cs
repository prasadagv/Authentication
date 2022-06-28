using Authentication.Domain.Contracts;
using Authentication.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticateService _service;
        private IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticateService service, IConfiguration configuration)
        {
            _logger = logger;
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("v1/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) && string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Invalid credentials");
            }            

            var resp = await _service.Authenticate(request);

            if (resp == "NotFound")
            {
                return NotFound("User or Password is invalid");
            }

            return Ok(resp);
        }
    }
}