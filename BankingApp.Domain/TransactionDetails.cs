using System;


namespace BankingApp.Domain
{
    public class TransactionDetails
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionTypeDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}
