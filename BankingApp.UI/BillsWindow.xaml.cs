using System.Windows;
using BankingApp.UI.ViewModels;

namespace BankingApp.UI
{
    /// <summary>
    /// Interaction logic for BillsWindow.xaml
    /// </summary>
    public partial class BillsWindow : Window
    {
        public BillsWindow(BillsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
