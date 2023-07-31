using System.Windows;
using BankingApp.BLL;
using BankingApp.UI.ViewModels;
using BankingApp.UI.Views;

namespace BankingApp.UI.NavigationServices
{
    public class NavigationService : INavigationService
    {
        private readonly UserService _userService;
        private readonly CustomerService _customerService;

        public Window LoginWindow { get; set; }

        public void Navigate(BaseViewModel viewModel)
        {
            switch (viewModel)
            {
                case LoginViewModel loginViewModel:
                    var loginWindow = new LoginWindow(loginViewModel);
                    loginWindow.Show();
                    break;
                case MainViewModel mainViewModel:
                    var mainWindow = new MainWindow(mainViewModel);
                    mainWindow.Show();
                    break;
                case SearchCustomersViewModel searchCustomersViewModel:
                    var searchCustomersWindow = new SearchCustomersWindow(searchCustomersViewModel);
                    searchCustomersWindow.Show();
                    break;
                case AddBillsViewModel addBillsViewModel:
                    var addBillsWindow = new AddBillsWindow(addBillsViewModel);
                    addBillsWindow.Show();
                    break;
                case BillsViewModel billsViewModel:
                    var billsWindow = new BillsWindow(billsViewModel);
                    billsWindow.Show();
                    break;
                case AddCustomersViewModel addCustomersViewModel:
                    var addCustomersWindow = new AddCustomersWindow(addCustomersViewModel);
                    addCustomersWindow.Show();
                    break;
                case ManageCustomersViewModel manageCustomersViewModel:
                    var manageCustomersWindow = new ManageCustomersWindow(manageCustomersViewModel);
                    manageCustomersWindow.Show();
                    break;
            }
        }
    }
}
