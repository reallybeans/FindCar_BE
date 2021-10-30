using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Repository;

namespace Tim_Xe.Service.Shared
{
    public static class ValiDatePhone
    {
        private static readonly TimXeDBContext context = new TimXeDBContext();
        public static bool CheckPhone(string phone)
        {
            try
            {
                var checkManger = context.Managers.FirstOrDefault(g => g.Phone.Contains(phone));
                var checkCustomer = context.Customers.FirstOrDefault(g => g.Phone.Contains(phone));
                var checkDriver = context.Drivers.FirstOrDefault(g => g.Phone.Contains(phone));
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
