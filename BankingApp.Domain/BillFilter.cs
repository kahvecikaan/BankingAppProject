using System;

namespace BankingApp.Domain
{
    public class BillFilter
    {
        public int? BillId { get; set; }
        public int? CustomerId { get; set; }  
        public DateTime? DateIssuedFrom { get; set; }   
        public DateTime? DateIssuedTo { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public decimal? AmountDueFrom { get; set; }
        public decimal? AmountDueTo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
