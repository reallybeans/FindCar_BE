using System.Collections.Generic;

namespace Tim_Xe.Data.Models
{
    public class TransactionListDataDTO
    {
        public string Message { get; set; }
        public IEnumerable<TransactionDTO> Data { get; set; }
        public string Status { get; set; }

        public TransactionListDataDTO(string message, IEnumerable<TransactionDTO> data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
