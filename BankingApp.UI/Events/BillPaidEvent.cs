using BankingApp.Domain;

namespace BankingApp.UI.Events
{
    public class BillPaidEvent
    {
        public Bill PaidBill { get; set; }
    }
}
