using System.Windows;
using BankingApp.UI.ViewModels;
namespace BankingApp.UI
{
    public partial class AddBillsWindow : Window
    {
        public AddBillsWindow(AddBillsViewModel viewModel)
        {
            InitializeComponent();
            viewModel.ClearFieldsAction = () =>
                            {
                                CustomerIdTextBox.Clear();
                                DateIssuedPicker.SelectedDate = null;
                                DueDatePicker.SelectedDate = null;
                                AmountDueTextBox.Clear();
                                BillStatusTextBox.Clear();
                            };
            DataContext = viewModel;
        }

        // Instead of lambda expression, we could also define a separate method
        // and in the AddBillsWindow, viewModel.ClearFieldsAction = ClearFields;
        //private void ClearFields()
        //{
        //    CustomerIdTextBox.Clear();
        //    DateIssuedPicker.SelectedDate = null;
        //    DueDatePicker.SelectedDate = null;
        //    AmountDueTextBox.Clear();
        //    BillStatusTextBox.Clear();
        //}
    }
}
