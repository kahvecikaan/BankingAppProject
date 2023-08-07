using System.Windows;
using BankingApp.UI.ViewModels;


namespace BankingApp.UI.Views
{
    public partial class AddTransactionsWindow : Window
    {
        public AddTransactionsWindow(AddTransactionsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseAction = Close;
        }
    }
}
