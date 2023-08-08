using BankingApp.Common.Events;
using BankingApp.Domain;


namespace BankingApp.BLL
{
    public class TransactionHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly TransactionService _transactionService;
        private readonly CustomerService _customerService;

        public TransactionHandler(IEventAggregator eventAggregator, TransactionService transactionService)
        {
            _eventAggregator = eventAggregator;
            _transactionService = transactionService;
            

            _eventAggregator.Subscribe<Common.BillPaidEvent>(OnBillPaid);
        }

        private void OnBillPaid(Common.BillPaidEvent billPaidEvent)
        {
            var transaction = new Transaction
            {
                TransactionType = 3,
                TransactionAmount = billPaidEvent.PaidBill.AmountDue,
                CustomerId = billPaidEvent.PaidBill.CustomerId,
                UserId = Common.UserSession.CurrentUser.UserId
            };
            _transactionService.InsertTransaction(transaction);
        }
    }
}
