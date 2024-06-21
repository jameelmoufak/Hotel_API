using Hotel_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AccountsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost("Authentication")]
        public ActionResult<HotelUser> Authenticate(AuthRequest request)
        {
            var user = ValidateUserInformation(request.UserName, request.Password);
            if (user == null) return Unauthorized();
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]));
            var signingCred = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() { new Claim("UserName", "JameelMoufak"), new Claim("Age","22"), new Claim("Course","11") };
            var securityToToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCred);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToToken);
            return Ok(token);
                
        }

        private object ValidateUserInformation(string userName, string password)
        {
            if (configuration["Account:Username"]==userName && configuration["Account:Password"] == password)
            {
                return new HotelUser() { UserId = 1, UserName = "JameelMoufak", FirstName = "Jameel", LastName = "Moufak" };
            }
            return null;
        }
    }
}
