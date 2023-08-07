using System;

namespace BankingApp.Domain
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
