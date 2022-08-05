using BackendProject.Data.Abstract;
using BackendProject.Data.Requests;
using BackendProject.Entity.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private IAuthRepository authRepository;

        private IConfiguration configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterRequest userForRegisterRequest)
        {
            if(await authRepository.UserExists(userForRegisterRequest.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists");

            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                UserName = userForRegisterRequest.UserName
            };

            var createdUser = await authRepository.Register(userToCreate, userForRegisterRequest.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserForLoginRequest userForLoginRequest)
        {
            var user = await authRepository.Login(userForLoginRequest.UserName, userForLoginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials =new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}
