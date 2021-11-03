using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.TransactionService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/transaction")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionServiceImp _transactionServiceImp;
        public TransactionsController()
        {
            _transactionServiceImp = new TransactionServiceImp();
        }
        [HttpGet]
        public async Task<TransactionListDataDTO> GetAllTransaction()
        {
            return await _transactionServiceImp.GetAllTransaction();
        }
        [HttpGet("get-by-customer/{id}")]
        public async Task<TransactionListDataDTO> GetTransactionsByCustomer(int id)
        {
            return await _transactionServiceImp.GetTransactionByCustomer(id);
        }
        [HttpGet("get-by-driver-{id}")]
        public async Task<TransactionListDataDTO> GetTransactionsByDriver(int id)
        {
            return await _transactionServiceImp.GetTransactionByDriver(id);
        }
    }
}
