using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class TransactionCreateDataDTO
    {
        public string Message { get; set; }
        public TransactionCreateDTO Data { get; set; }
        public string Status { get; set; }

        public TransactionCreateDataDTO(string message, TransactionCreateDTO data, string status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
