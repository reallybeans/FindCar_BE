using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;
using System.Threading.Tasks;

namespace Tim_Xe.Service.LoginService
{
    public class LoginServiceImp
    {
        private readonly TimXeDBContext context;
        public LoginServiceImp()
        {
            context = new TimXeDBContext();
        }
        public async Task<UserWithToken> LoginAsync(Login account)
        {
            var existingAccount = await context.Managers.Include(a => a.Role).FirstOrDefaultAsync(a => a.Email == account.Email
                       && a.Password == account.Password);
            UserWithToken userWithToken = new UserWithToken(existingAccount, null);
           
            return userWithToken;
        }


        public async Task<UserWithToken> LoginDriverAsync(LoginDriverDTO loginDriver)
        {
            Driver existingAccount = new Driver();
            UserWithToken userWithToken ;
            if (loginDriver.Token != null)
            {
                var stream = loginDriver.Token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
                existingAccount = await context.Drivers.FirstOrDefaultAsync(d => d.Email == email
                      && d.IsDeleted == false);
                return userWithToken = new UserWithToken(null,existingAccount) ;
            }
            else if (loginDriver.Phone != null)
            {
                existingAccount = await context.Drivers.FirstOrDefaultAsync(d => d.Phone == loginDriver.Phone
                      && d.IsDeleted == false);
                return userWithToken = new UserWithToken(null, existingAccount);
            }
            else return null;
           
        }

        public async Task<UserWithToken> LoginWithTokenWeb(string token)
        {
            UserWithToken userWithToken;
            Manager existingAccount = new Manager();
            if (token != null)
            {
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
                existingAccount = await context.Managers.Include(a => a.Role).FirstOrDefaultAsync(a => a.Email == email);
                return userWithToken = new UserWithToken(existingAccount, null);
            }
            else return null;

        }

    }
}
