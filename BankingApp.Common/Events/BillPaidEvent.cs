using BankingApp.Domain;

namespace BankingApp.Common
{
    public class BillPaidEvent
    {
        public Bill PaidBill { get; set; }
    }
}
