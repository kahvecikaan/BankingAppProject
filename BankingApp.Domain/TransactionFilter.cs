using System;

namespace BankingApp.Domain
{
    public class TransactionFilter
    {   
        public int? TransactionId { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public decimal? TransactionAmountFrom { get; set; }
        public decimal? TransactionAmountTo { get; set; }
        public int? TransactionType { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}
