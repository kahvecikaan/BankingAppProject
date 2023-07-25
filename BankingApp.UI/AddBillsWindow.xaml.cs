using System.Windows;
using BankingApp.UI.ViewModels;
namespace BankingApp.UI
{
    /// <summary>
    /// Interaction logic for AddBillsWindow.xaml
    /// </summary>
    public partial class AddBillsWindow : Window
    {
        public AddBillsWindow(AddBillsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
