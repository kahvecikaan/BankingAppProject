using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ManageTransactionsWindow.xaml
    /// </summary>
    public partial class ManageTransactionsWindow : Window
    {
        public ManageTransactionsWindow(ManageTransactionsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
