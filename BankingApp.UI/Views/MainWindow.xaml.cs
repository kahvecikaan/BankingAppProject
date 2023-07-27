using System.Windows;

namespace BankingApp.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(object viewModel) : this()
        {
            DataContext = viewModel;
        }
    }

}
