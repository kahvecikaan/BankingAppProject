using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using System.Collections.ObjectModel;
using BankingApp.Common.Events;
using BankingApp.UI.Events;

namespace BankingApp.UI.ViewModels
{
    public class ManageTransactionsViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;
        private readonly CustomerService _customerService;
        private readonly ParameterService _parameterService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private TransactionFilter _transactionFilter;
        public TransactionFilter TransactionFilter
        {
            get { return _transactionFilter; }
            set
            {
                _transactionFilter = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand SearchTransactionsCommand { get; set; }
        public ObservableCollection<TransactionDetails> Transactions { get; set; }
        public ObservableCollection<Parameter> TransactionTypes { get; set; }
        public RelayCommand NavigateToAddTransactionCommand { get; set; }
        public ManageTransactionsViewModel(TransactionService transactionService, CustomerService customerService, ParameterService parameterService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _parameterService = parameterService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<TransactionAddedEvent>(OnTransactionAdded);

            Transactions = new ObservableCollection<TransactionDetails>(_transactionService.FetchAllTransactionDetails());

            TransactionFilter = new TransactionFilter();

            TransactionTypes = new ObservableCollection<Parameter>(_parameterService.FetchParametersByType("TransactionType"));

            SearchTransactionsCommand = new RelayCommand(SearchTransactions, _ => true);
            NavigateToAddTransactionCommand = new RelayCommand(NavigateToAddTransaction, _ => true);
        }
        
        private void SearchTransactions(object obj)
        {
            var transactions = _transactionService.SearchTransactionDetails(TransactionFilter);
            Transactions.Clear();

            foreach (var transaction in transactions)
            {
                Transactions.Add(transaction);
            }

            OnPropertyChanged(nameof(Transactions)); // Notify UI about the changes
        }

        private void NavigateToAddTransaction(object obj)
        {
            var addTransactionsViewModel = new AddTransactionsViewModel(_transactionService, _customerService, _parameterService, _eventAggregator);
            _navigationService.Navigate(addTransactionsViewModel);
        }

        private void OnTransactionAdded(TransactionAddedEvent transactionAddedEvent)
        {
            // Refresh the Transactions collection
            Transactions = new ObservableCollection<TransactionDetails>(_transactionService.FetchAllTransactionDetails());

            // Notify any data bindings that the Transactions property has changed
            OnPropertyChanged(nameof(Transactions));
        }
    }
}
