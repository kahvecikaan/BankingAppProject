using BankingApp.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BankingApp.UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();
            viewModel.ClearPasswordAction = () => PasswordTextBox.Clear();
            viewModel.CloseAction = new System.Action(this.Close);
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; 
            }
        }
    }
}
