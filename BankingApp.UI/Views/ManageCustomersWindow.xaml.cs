using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ManageCustomersWindow.xaml
    /// </summary>
    public partial class ManageCustomersWindow : Window
    {
        public ManageCustomersWindow(ManageCustomersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
