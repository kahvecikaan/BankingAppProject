using System.Windows;
using BankingApp.BLL;
using BankingApp.DAL;
using BankingApp.UI.NavigationServices;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UserService userService = new UserService(new UserData());
            CustomerService customerService = new CustomerService(new CustomerData());
            BillService billService = new BillService(new BillData(), customerService);

            INavigationService navigationService = new NavigationService();

            LoginViewModel loginViewModel = new LoginViewModel(userService, customerService, billService, navigationService);

            navigationService.Navigate(loginViewModel);
        }
    }
}
