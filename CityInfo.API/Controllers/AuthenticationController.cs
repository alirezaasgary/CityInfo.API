using CityInfo.API.Models_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequerstBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequerstBody user)
        {
            var Validateuser = ValidatUserCeridential(user.UserName, user.Password);
            return "";
        }

        private CityInfoUser ValidatUserCeridential(string userName,string password)
        {
            return null;
        }
    }
}
