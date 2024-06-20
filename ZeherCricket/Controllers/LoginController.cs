using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZeherCricket.Data;

namespace ZeherCricket.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;

        private ZeherCricketDBContext _zeherCricketDBContext;

        public LoginController(IConfiguration configuration, ZeherCricketDBContext zeherCricketDBContext)
        {
            _zeherCricketDBContext = zeherCricketDBContext;
            _configuration = configuration;
        }

        [NonAction]
        public string GenerateJWTToken(LoginInfo loginInfo)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
            var tokenHandler = new JwtSecurityTokenHandler();
            Claim roleClaim;

            if (loginInfo.userName == "HarshTest")
            {
                // is admin
                roleClaim = new Claim(ClaimTypes.Role, "Admin");
            }
            else
            {
                roleClaim = new Claim(ClaimTypes.Role, "User");
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, loginInfo.userName),
                        roleClaim,
                    }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenGenerated = tokenHandler.WriteToken(token);

            return tokenGenerated;
        }

        [NonAction]
        public bool isExistingUser(LoginInfo loginInfo)
        {
            return _zeherCricketDBContext.UsersTable.Find(loginInfo.userName) != null;
        }

        [Route("Login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Login(LoginInfo loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username and password");
            }

            if (_zeherCricketDBContext.UsersTable.Where(user => user.UserName == loginInfo.userName && user.Password == loginInfo.password).Count() == 1)
            {
                // user exists in the database and has entered correct password
                string generatedToken = GenerateJWTToken(loginInfo);

                return Ok(new LoginResponse
                {
                    UserName = loginInfo.userName,
                    token = generatedToken
                });
            }
            else
            {
                return NotFound("Invalid username or password");
            }
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult RegisterNewUser(LoginInfo loginInfo)
        {
            Console.WriteLine("entering this method");
            if (isExistingUser(loginInfo))
            {
                return Conflict(new RegisterError
                {
                    Message = "User Already Exists"
                });
            }
            _zeherCricketDBContext.UsersTable.Add(new UserInfo
            {
                UserName = loginInfo.userName,
                Password = loginInfo.password
            });

            _zeherCricketDBContext.SaveChanges();

            string tokenGenerated = GenerateJWTToken(loginInfo);
            Console.WriteLine(tokenGenerated);

            return Ok(new RegisterResponse
            {
                userName = loginInfo.userName,
                token = tokenGenerated
            });
        }
    }

    public class LoginResponse
    {
        public string UserName { get; set; }

        public string token { get; set; }
    }

    public class RegisterError
    {
        public string Message { get; set; }
    }

    public class RegisterResponse
    {
        public string userName { get; set; }

        public string token { get; set; }
    }
}
