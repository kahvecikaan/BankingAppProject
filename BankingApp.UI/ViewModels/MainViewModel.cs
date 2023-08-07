using BankingApp.BLL;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using BankingApp.Common.Events;

namespace BankingApp.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand LogoutCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly UserService _userService;
        private readonly CustomerService _customerService;
        private readonly BillService _billService;
        private readonly ParameterService _parameterService;
        private readonly IEventAggregator _eventAggregator;
        private readonly TransactionService _transactionService;

        // public RelayCommand NavigateToSearchCommand { get; set; }
        // public RelayCommand NavigateToAddBillCommand { get; set; }
        public RelayCommand NavigateToBillsCommand { get; set; }
        public RelayCommand NavigateToManageCustomersCommand { get; set; }
        public RelayCommand NavigateToManageTransactionsCommand { get; set; }

        public MainViewModel(UserService userService, CustomerService customerService, BillService billService, ParameterService parameterService, INavigationService navigationService, IEventAggregator eventAggregator, TransactionService transactionService)
        {
            _userService = userService;
            _customerService = customerService;
            _billService = billService;
            _parameterService = parameterService;
            _transactionService = transactionService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            LogoutCommand = new RelayCommand(Logout, _ => true);
            // NavigateToSearchCommand = new RelayCommand(NavigateToSearch, _ => true);
            // NavigateToAddBillCommand = new RelayCommand(NavigateToAddBill, _ => true);

            NavigateToBillsCommand = new RelayCommand(NavigateToBills, _ => true);
            NavigateToManageCustomersCommand = new RelayCommand(NavigateToManageCustomers, _ => true);
            NavigateToManageTransactionsCommand = new RelayCommand(NavigateToManageTransactions, _ => true);
        }

        //private void NavigateToSearch(object obj)
        //{
        //    var searchCustomersViewModel = new SearchCustomersViewModel(_customerService, _navigationService);
        //    _navigationService.Navigate(searchCustomersViewModel);
        //}

        //private void NavigateToAddBill(object obj)
        //{
        //    var addBillsViewModel = new AddBillsViewModel(_billService, _customerService, _parameterService, _navigationService, _eventAggregator);
        //    _navigationService.Navigate(addBillsViewModel);
        //}

        private void NavigateToBills(object obj)
        {
            var billsViewModel = new BillsViewModel(_billService, _customerService, _parameterService, _navigationService, _eventAggregator);
            _navigationService.Navigate(billsViewModel);
        }

        private void NavigateToManageCustomers(object obj)
        {
            var manageCustomersViewModel = new ManageCustomersViewModel(_customerService, _parameterService, _navigationService, _eventAggregator);
            _navigationService.Navigate(manageCustomersViewModel);
        }

        private void NavigateToManageTransactions(object obj)
        {
            var manageTransactionsViewModel = new ManageTransactionsViewModel(_transactionService, _customerService, _parameterService, _navigationService, _eventAggregator);
            _navigationService.Navigate(manageTransactionsViewModel);
        }

        private void Logout(object obj)
        {
            LoginViewModel loginViewModel = new LoginViewModel(_userService, _customerService, _billService, _parameterService, _navigationService, _eventAggregator, _transactionService);
            _navigationService.Navigate(loginViewModel);
        }
    }
}
