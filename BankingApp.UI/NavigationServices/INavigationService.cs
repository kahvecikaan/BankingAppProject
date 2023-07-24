
using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI.NavigationServices
{
    public interface INavigationService
    {
        Window LoginWindow { get; set; }
        public void Navigate(BaseViewModel viewModel);
    }
}
