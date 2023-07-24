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

        public RelayCommand NavigateToSearchCommand { get; set; }

        public MainViewModel(UserService userService, CustomerService customerService, INavigationService navigationService)
        {
            _userService = userService;
            _customerService = customerService;
            _navigationService = navigationService;
            LogoutCommand = new RelayCommand(Logout, _ => true);
            NavigateToSearchCommand = new RelayCommand(NavigateToSearch, _ => true);
        }

        private void NavigateToSearch(object obj)
        {
            var searchCustomersViewModel = new SearchCustomersViewModel(_customerService, _navigationService);
            _navigationService.Navigate(searchCustomersViewModel);
        }

        private void Logout(object obj)
        {
            LoginViewModel loginViewModel = new LoginViewModel(_userService, _customerService, _navigationService);
            _navigationService.Navigate(loginViewModel);
        }

    }
}
