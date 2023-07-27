using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI.Views
{
    public partial class BillsWindow : Window
    {
        public BillsWindow(BillsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
