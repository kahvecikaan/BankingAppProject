using BankingApp.BLL;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;

namespace BankingApp.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand LogoutCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly UserService _userService;
        private readonly CustomerService _customerService;
        private readonly BillService _billService;

        public RelayCommand NavigateToSearchCommand { get; set; }
        public RelayCommand NavigateToAddBillCommand { get; set; }

        public MainViewModel(UserService userService, CustomerService customerService, BillService billService, INavigationService navigationService)
        {
            _userService = userService;
            _customerService = customerService;
            _billService = billService;
            _navigationService = navigationService;
            LogoutCommand = new RelayCommand(Logout, _ => true);
            NavigateToSearchCommand = new RelayCommand(NavigateToSearch, _ => true);
            NavigateToAddBillCommand = new RelayCommand(NavigateToAddBill, _ => true);
        }

        private void NavigateToSearch(object obj)
        {
            var searchCustomersViewModel = new SearchCustomersViewModel(_customerService, _navigationService);
            _navigationService.Navigate(searchCustomersViewModel);
        }

        private void NavigateToAddBill(object obj)
        {
            var addBillsViewModel = new AddBillsViewModel(_billService, _customerService, _navigationService);
            _navigationService.Navigate(addBillsViewModel);
        }

        private void Logout(object obj)
        {
            LoginViewModel loginViewModel = new LoginViewModel(_userService, _customerService, _billService, _navigationService);
            _navigationService.Navigate(loginViewModel);
        }

    }
}
