using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository.Entities;
using Tim_Xe.Service.LoginService;

namespace TimXe.Present.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class loginsController : ControllerBase
    {
        private readonly LoginServiceImp _loginServiceImp;

        private readonly JWTSettings _jwtsettings;
        public loginsController(IOptions<JWTSettings> jwtsettings)
        {
            _loginServiceImp = new LoginServiceImp();
            _jwtsettings = jwtsettings.Value;
        }
        //POST: api/Accounts
        [HttpPost("loginweb")]
        public async Task<ActionResult<UserWithToken>> LoginAsync([FromBody] Login login)
        {
           // UserWithToken userWithToken = new UserWithToken(null);
            var userWithToken = await _loginServiceImp.LoginAsync(login);
            if (userWithToken == null)
            {
                return NotFound();
            }
            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(userWithToken);
            return userWithToken;
        }
        [HttpPost("logindriver")]
        public async Task<ActionResult<UserWithToken>> LoginDriverAsync([FromBody] LoginDriverDTO login)
        {
            var userWithToken = await _loginServiceImp.LoginDriverAsync(login);
            if (userWithToken == null)
            {
                return NotFound();
            }
           
            
            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(userWithToken);
            return userWithToken;
        }
        private string GenerateAccessToken(UserWithToken accounts)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", Convert.ToString(accounts.Email)),
                    new Claim("role", Convert.ToString(accounts.Role))
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
