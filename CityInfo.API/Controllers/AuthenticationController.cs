using CityInfo.API.Models_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            return null;
        }

        private CityInfoUser ValidatUserCeridential(string userName,string password)
        {
            return new CityInfoUser(

                 1,
                 userName,
                 "",
                 "",
                 ""
                );
            
            return null;
        }
    }
}
