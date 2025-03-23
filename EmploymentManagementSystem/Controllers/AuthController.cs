using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmploymentManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<Employee> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new Employee { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new { message = "Invalid email or password." });

            // Check if user has "User" role
            if (!await _userManager.IsInRoleAsync(user, "User"))
                return Unauthorized(new { message = "Access denied. You are not assigned the required role." });

            var roles = await _userManager.GetRolesAsync(user);


            var token = GenerateJwtToken(user);
            var result = new AuthModel
            {
                IsAuthenticated = true,
                UserID = user.Id,
                Token = token.Result,
                Email = user.Email,
                UserName = user?.UserName,

                Roles = roles.ToList()
            };



            return Ok(new { result });
        }


        [HttpPost("login/admin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModelDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new { message = "Invalid email or password." });

            // Check if user has "User" role
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
                return Unauthorized(new { message = "Access denied. You are not assigned the required role." });

            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateJwtToken(user);
            var result = new AuthModel
            {
                IsAuthenticated = true,
                UserID = user.Id,
                Token = token.Result,
                Email = user.Email,
                UserName = user?.UserName,

                Roles = roles.ToList()
            };



            return Ok(new { result });
        }

        private async Task<string> GenerateJwtToken(Employee user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var roles = await _userManager.GetRolesAsync(user); // Get user roles

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email)
    };

            // Add each role as a separate claim
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudiance"],
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
