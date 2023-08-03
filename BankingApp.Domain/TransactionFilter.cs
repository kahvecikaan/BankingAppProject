using System;

namespace BankingApp.Domain
{
    public class TransactionFilter
    {   
        public int? TransactionId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? TransactionAmountFrom { get; set; }
        public decimal? TransactionAmountTo { get; set; }
        public string TransactionType { get; set; } // or it could be int if you prefer using the Code column in the Parameters table.
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime? TransactionDateFrom { get; set; }
        public DateTime? TransactionDateTo { get; set; }
    }
}
