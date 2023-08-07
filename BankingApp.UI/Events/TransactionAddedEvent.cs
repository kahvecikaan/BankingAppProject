using BankingApp.Domain;

namespace BankingApp.UI.Events
{
    public class TransactionAddedEvent
    {
        public Transaction AddedTransaction { get; set; }
    }
}
