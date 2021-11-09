using System;

namespace Tim_Xe.Data.Models
{
    public partial class RefreshToken
    {
        public int TokenId { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        //public virtual AccountDTO Account { get; set; }
    }
}
