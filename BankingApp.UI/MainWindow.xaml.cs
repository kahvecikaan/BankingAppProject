using System.Windows;

namespace BankingApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
