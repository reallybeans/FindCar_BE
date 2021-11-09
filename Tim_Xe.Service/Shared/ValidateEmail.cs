using System;
using System.Linq;
using Tim_Xe.Data.Repository;

namespace Tim_Xe.Service.Shared
{
    public static class ValidateEmail
    {
        private static readonly TimXeDBContext context = new TimXeDBContext();
        public static bool CheckEmail(string email)
        {
            try
            {
                var checkManger = context.Managers.FirstOrDefault(g => g.Email.Contains(email));
                var checkCustomer = context.Customers.FirstOrDefault(g => g.Email.Contains(email));
                var checkDriver = context.Drivers.FirstOrDefault(g => g.Email.Contains(email));
                if (checkManger != null)
                {
                    return false;
                }
                else if (checkCustomer != null)
                {
                    return false;
                }
                else if (checkDriver != null)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
