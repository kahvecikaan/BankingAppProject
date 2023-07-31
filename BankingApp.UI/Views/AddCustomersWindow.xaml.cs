using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI.Views
{
    /// <summary>
    /// Interaction logic for AddCustomersWindow.xaml
    /// </summary>
    public partial class AddCustomersWindow : Window
    {
        public AddCustomersWindow(AddCustomersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseAction = Close;
        }
    }
}
