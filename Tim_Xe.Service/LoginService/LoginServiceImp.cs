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
using BCryptNet = BCrypt.Net.BCrypt;
namespace Tim_Xe.Service.LoginService
{
    public class LoginServiceImp : ILoginService
    {
        private readonly TimXeDBContext context;
        public LoginServiceImp()
        {
            context = new TimXeDBContext();
        }
        public async Task<UserWithTokenDataDTO> LoginCustomerAsync(Login account)
        {
            try {
                var existingAccount = await context.Customers.FirstOrDefaultAsync(a => a.Email == account.Email);                
                if(existingAccount == null)
                {
                    return new UserWithTokenDataDTO("login fail", null, "fail");
                }
                else
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(account.Password, existingAccount.Password);
                    if (!verified) return new UserWithTokenDataDTO("login fail", null, "fail");
                    else
                    {
                        UserWithToken userWithToken = new UserWithToken(null, null, existingAccount);
                        return new UserWithTokenDataDTO("login success", userWithToken, "success");
                    }                   
                }                
            } catch (Exception e)
            {
                return new UserWithTokenDataDTO("login fail", null, "fail");
            }

            
        }
        public async Task<UserWithTokenDataDTO> LoginAsync(Login account)
        {
            try {
                var existingAccount = await context.Managers.Include(a => a.Role).FirstOrDefaultAsync(a => a.Email == account.Email);                
                if(existingAccount == null)
                {
                    return new UserWithTokenDataDTO("login fail", null, "fail");
                }
                else
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(account.Password, existingAccount.Password);
                    if (!verified) return new UserWithTokenDataDTO("login fail", null, "fail");
                    else
                    {
                        UserWithToken userWithToken = new UserWithToken(existingAccount,null, null);
                        return new UserWithTokenDataDTO("login success", userWithToken, "success");
                    }                   
                }                
            } catch (Exception e)
            {
                return new UserWithTokenDataDTO("login fail", null, "fail");
            }

            
        }


        public async Task<UserWithTokenDataDTO> LoginDriverAsync(LoginDriverDTO loginDriver)
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
                userWithToken = new UserWithToken(null, existingAccount, null); ; ;
                return new UserWithTokenDataDTO("login success", userWithToken, "success");
            }
            else if (loginDriver.Phone != null)
            {
                existingAccount = await context.Drivers.FirstOrDefaultAsync(d => d.Phone == loginDriver.Phone
                      && d.IsDeleted == false);
                if(existingAccount == null){
                    return new UserWithTokenDataDTO("login fail", null, "fail");
                }
                else
                {
                    userWithToken = new UserWithToken(null, existingAccount,null);
                    return new UserWithTokenDataDTO("login success", userWithToken, "success");
                }
            }
            else return new UserWithTokenDataDTO("login fail", null, "fail");

        }

        public async Task<UserWithTokenDataDTO> LoginWithTokenWeb(string token)
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
                userWithToken = new UserWithToken(existingAccount, null,null);
                return new UserWithTokenDataDTO("login success", userWithToken, "success");
            }
            else return new UserWithTokenDataDTO("login fail", null, "fail");

        }

    }
}
