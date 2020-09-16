using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PartiuAlmoco.Core.Domain.Interfaces;
using PartiuAlmoco.Web.Api.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices loginServices;
        private readonly ILogger<LoginController> logger;

        [HttpGet]
        [Route(nameof(GetJwtToken))]
        public ActionResult GetJwtToken(string email, string password)
        {
            try
            {
                var user = loginServices.Authenticate(email, password);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                        new Claim("FriendlyName", user.FriendlyName),
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);
                    var algorithm = SecurityAlgorithms.HmacSha256;

                    var signingCredentials = new SigningCredentials(key, algorithm);

                    var token = new JwtSecurityToken(
                        Constants.Issuer,
                        Constants.Audiance,
                        claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials);

                    var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { access_token = tokenJson, user_info = user });
                }

                return NotFound($"User with e-mail '{email}' was not found or the password is invalid.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult CreateUser(CreateUserDTO user)
        {
            try
            {
                return CreatedAtAction(nameof(CreateUser), loginServices.CreateUser(user.FullName, user.FriendlyName, user.Email, user.Password));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        public LoginController(ILoginServices loginServices, ILogger<LoginController> logger)
        {
            this.loginServices = loginServices ?? throw new ArgumentNullException(nameof(loginServices));
            this.logger = logger;
        }
    }
}
