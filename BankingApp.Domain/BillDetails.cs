using System;

namespace BankingApp.Domain
{
    public class BillDetails
    {
        public int BillId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }
        public string BillStatusDescription { get; set; }

        public Bill ToBill()
        {
            return new Bill
            {
                BillId = this.BillId,
                CustomerId = this.CustomerId,
                DateIssued = this.DateIssued,
                DueDate = this.DueDate,
                AmountDue = this.AmountDue,
                BillStatus = this.BillStatusDescription == "Due" ? 1 : 2
            };
        }
    }
}
