using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI.Views
{
    public partial class SearchCustomersWindow : Window
    {
        public SearchCustomersWindow(SearchCustomersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
