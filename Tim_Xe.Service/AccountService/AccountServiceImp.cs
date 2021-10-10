
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Tim_Xe.Data.Models;
//using Tim_Xe.Data.Repository;
//using Tim_Xe.Data.Repository.Entities;
//using AutoMapper.QueryableExtensions;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;
//using System.Threading.Tasks;

//namespace Tim_Xe.Service.AccountService
//{
//    public class AccountServiceImp
//    {
//        private readonly TimXeDBContext db;
//        private readonly AccountMapping accountMapping;
//        public AccountServiceImp()
//        {
//            db = new TimXeDBContext();
//            accountMapping = new AccountMapping();
//        }
//        public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
//        {
//            var result = db.Accounts.ProjectTo<AccountDTO>(accountMapping.configAccount).ToListAsync(); 
//            return await result;
//        }
//        public async Task<AccountDTO> GetAccountByIdAsync(int id)
//        {
//            var result = db.Accounts.ProjectTo<AccountDTO>(accountMapping.configAccount).Where(a => a.Id == id).FirstOrDefaultAsync() ; 
//            return await result;

//        }
//        public async Task<int> CreateAccount(AccountCreateDTO account)
//        {

//            db.Accounts.Add(new Account(){
//                Name = account.Name,
//                Phone = account.Phone,
//                Password = account.Password,
//                Email = account.Email,
//                RoleId = 2,
//                Status = true,
//                CreateAt = DateTime.Now,
//            });

//            return await db.SaveChangesAsync();

//        }
//        public async Task<int> UpdateAccount(AccountUpdateDTO account)
//        {
//            var existingAccount = db.Accounts.Where(a => a.Id == account.Id)
//                                                    .FirstOrDefault();

//            if (existingAccount != null)
//            {
//                existingAccount.Name = account.Name;
//                existingAccount.Password = account.Password;
//                existingAccount.Phone = account.Phone;
//                existingAccount.RoleId = (int)account.RoleId;
//                existingAccount.Status = (bool)account.Status;
//            }
//            else
//            {
//                return 0;
//            }

//            return await db.SaveChangesAsync();

//        }
//        public Task<int> DeleteAccountAsync(int id)
//        {
//            var existingAccount = db.Accounts.Where(a => a.Id == id)
//                                                    .FirstOrDefault();

//            if (existingAccount != null)
//            {
//                existingAccount.Status = false;
//            }
//            else
//            {
//                return null;
//            }

//            return db.SaveChangesAsync();

//        }
//    }
//}
