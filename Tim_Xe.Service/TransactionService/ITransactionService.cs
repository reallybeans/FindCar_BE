using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.TransactionService
{
    public interface ITransactionService
    {
        Task<TransactionListDataDTO> GetAllTransaction();
        Task<TransactionListDataDTO> GetTransactionByCustomer(int id);
        Task<TransactionListDataDTO> GetTransactionByDriver(int id);
    }
}
