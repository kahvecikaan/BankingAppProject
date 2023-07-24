using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI
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
