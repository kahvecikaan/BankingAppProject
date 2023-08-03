using System;

namespace BankingApp.Domain
{
    public class Bill
    {
        public int BillId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }
        public int BillStatus { get; set; }
    }
}
