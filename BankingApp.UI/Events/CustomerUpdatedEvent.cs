using BankingApp.Domain;

namespace BankingApp.UI.Events
{
    public class CustomerUpdatedEvent
    {
        public Customer UpdatedCustomer { get; set; }
    }
}
