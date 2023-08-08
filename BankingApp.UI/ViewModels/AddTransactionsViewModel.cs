using System;
using System.Windows;
using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using System.Linq;
using BankingApp.Common.Events;
using System.Collections.ObjectModel;
using BankingApp.UI.Events;

namespace BankingApp.UI.ViewModels
{
    public class AddTransactionsViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;
        private readonly CustomerService _customerService;
        private readonly ParameterService _parameterService;
        private readonly IEventAggregator _eventAggregator;
        private int _customerId;
        private decimal _transactionAmount;
        private TransactionTypeItem _transactionType;
        
        public System.Action CloseAction { get; set; }
        public RelayCommand AddTransactionCommand { get; set; }

        public ObservableCollection<TransactionTypeItem> TransactionTypeParameters { get; set; }

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                _customerId = value;
                OnPropertyChanged();
            }
        }

        public decimal TransactionAmount
        {
            get { return _transactionAmount; }
            set
            {
                _transactionAmount = value;
                OnPropertyChanged();
            }
        }

        public TransactionTypeItem TransactionType
        {
            get { return _transactionType; }
            set
            {
                _transactionType = value;
                OnPropertyChanged();
            }
        }

        public AddTransactionsViewModel(TransactionService transactionService, CustomerService customerService, ParameterService parameterService, IEventAggregator eventAggregator)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _parameterService = parameterService;
            _eventAggregator = eventAggregator;

            var parameters = _parameterService.FetchParametersByType("TransactionType");
            TransactionTypeParameters = new ObservableCollection<TransactionTypeItem>
                (parameters.Where(p => p.Code != 3) // Excluding "BillPayment"
                .Select(p => new TransactionTypeItem { Code = p.Code, Description = p.Description }));

            AddTransactionCommand = new RelayCommand(AddTransaction, CanAddTransaction);
            TransactionType = TransactionTypeParameters.First();
        }

        public void AddTransaction(object parameter)
        {
            if (_customerService.IsCustomerExist(CustomerId))
            {
                var newTransaction = new Transaction
                {
                    CustomerId = this.CustomerId,
                    TransactionAmount = this.TransactionAmount,
                    TransactionType = TransactionType.Code,
                    UserId = Common.UserSession.CurrentUser.UserId,
                    TransactionDate = DateTime.Now
                };

                _transactionService.InsertTransaction(newTransaction);

                MessageBox.Show("New transaction added successfully!");

                _eventAggregator.Publish(new TransactionAddedEvent { AddedTransaction = newTransaction });

                CloseAction?.Invoke();
            }

            else
            {
                MessageBox.Show("The provided CustomerId does not exist. Please try again.", "Invalid CustomerID", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanAddTransaction(object parameter)
        {
            return true;
        }
    }
}
