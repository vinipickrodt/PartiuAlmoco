﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices loginServices;

        [HttpGet]
        [Route("GetJwtToken")]
        public IActionResult GetJwtToken(string email, string password)
        {
            var user = loginServices.Authenticate(email, password);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.FullName),
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

                return Ok(new { access_token = tokenJson });
            }

            return NotFound();
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser(CreateUserDTO user)
        {
            return CreatedAtAction(nameof(CreateUser), loginServices.CreateUser(user.FullName, user.FriendlyName, user.Email, user.Password));
        }

        public LoginController(ILoginServices loginServices)
        {
            this.loginServices = loginServices ?? throw new ArgumentNullException(nameof(loginServices));
        }
    }
}
