using Microsoft.AspNetCore.Mvc;
using UserAuthentication.API.DTOs;
using UserAuthentication.API.Handlers;
using UserAuthentication.API.Repositories;

namespace UserAuthentication.API.Controllers
{
    [ApiController]
    // [Route("auth")] or
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {   
           // check if user is authenticated i.e. check username and password
           var user = await userRepository.
                                       AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if (user != null)
            {
                // generate a JWT Token
               var clientToken = await tokenHandler.CreateTokenAsync(user);
                return Ok(clientToken);
            }

            return BadRequest("Username or Password is incorrect.");
        }
    }
}
