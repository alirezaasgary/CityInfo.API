using CityInfo.API.Models_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
           _configuration= configuration;
        }
        public class AuthenticationRequerstBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequerstBody user)
        {
            var Validateuser = ValidatUserCeridential(user.UserName, user.Password);
            if (Validateuser == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCridential= new SigningCredentials(
                securityKey,SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("UserName",user.UserName.ToString()));
            //claimsForToken.Add(new Claim("UserName",user..ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCridential);
              var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);  

            return token;
        }

        private CityInfoUser ValidatUserCeridential(string userName,string password)
        {
            return new CityInfoUser(

                 1,
                 userName,
                 "Alireza",
                 "Askari",
                 "Moscow"
                );
            
            return null;
        }
    }
}
