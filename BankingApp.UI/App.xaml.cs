using System.Windows;
using BankingApp.BLL;
using BankingApp.DAL;
using BankingApp.UI.NavigationServices;
using BankingApp.UI.ViewModels;
using BankingApp.UI.Events;

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
            ParameterService parameterService = new ParameterService(new ParameterData());

            IEventAggregator eventAggregator = new EventAggregator();

            INavigationService navigationService = new NavigationService();

            LoginViewModel loginViewModel = new LoginViewModel(userService, customerService, billService, parameterService, navigationService, eventAggregator);

            navigationService.Navigate(loginViewModel);
        }
    }
}
