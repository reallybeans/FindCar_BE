using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.LoginService
{
    public interface ILoginService
    {
        Task<UserWithTokenDataDTO> LoginCustomerAsync(Login account);
        Task<UserWithTokenDataDTO> LoginAsync(Login account);
        Task<UserWithTokenDataDTO> LoginDriverAsync(LoginDriverDTO loginDriver);
        Task<UserWithTokenDataDTO> LoginWithTokenWeb(string token);
    }
}
