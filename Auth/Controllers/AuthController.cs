using Auth.Abstractions;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        public IAuthService AuthService { get; }

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            string token = AuthService.Login(loginRequest.UserName, loginRequest.Password);
            return Ok(token);
        }

    }
}
