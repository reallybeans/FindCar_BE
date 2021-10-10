
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Tim_Xe.Data.Models;
//using Tim_Xe.Service.AccountService;

//namespace TimXe.Present.Controllers.V1
//{
//    [Authorize(Roles = "admin")]
//    [Authorize(Roles = "group")]
//    [Authorize(Roles = "owner")]
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class accountsController : ControllerBase
//    {
//        private readonly AccountServiceImp _accountServiceImp;
        
//        public accountsController()
//        {
//            _accountServiceImp = new AccountServiceImp();
//        }

//        [HttpPost]
//        public async Task<ActionResult<int>> CreateAsync(AccountCreateDTO account)
//        {
//            if (!ModelState.IsValid)

//                return BadRequest("Invalid data.");

            

//            return await _accountServiceImp.CreateAccount(account);
//        }

//        //GET: api/<AccountDTO[]>
//        [HttpGet]
//        public async Task<IEnumerable<AccountDTO>> GetAll()
//        {
//            return await _accountServiceImp.GetAllAccountsAsync();
//        }
//        //GET: api/<AccountDTO[]>
//        [HttpGet("{id}")]
//        public async Task<AccountDTO> GetAsync(int id)
//        {  
//            return await _accountServiceImp.GetAccountByIdAsync(id);
//        }

//        [HttpPut]
//        public async Task<ActionResult<int>> UpdateAsync(AccountUpdateDTO account)
//        {
//            if (!ModelState.IsValid)

//                return BadRequest("Invalid data.");


//            return await _accountServiceImp.UpdateAccount(account);
//        }

//        [HttpDelete("{id}")]
//        public ActionResult<int> Delete(int id)
//        {
//            if (id != 0)
//            {
//                _accountServiceImp.DeleteAccountAsync(id);
//                return Ok();
//            };
//            return NotFound();

//        }
//    }
//}
