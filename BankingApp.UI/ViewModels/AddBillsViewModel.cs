using System;
using System.Windows;
using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;

namespace BankingApp.UI.ViewModels
{
    public class AddBillsViewModel : BaseViewModel
    {
        private readonly BillService _billService;
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;
        // private readonly UserService _userService;
        private int _customerId;
        private DateTime _dateIssued;
        private DateTime _dueDate;
        private decimal _amountDue;
        private string _billStatus;
        private Bill _selectedBill;

        public System.Action ClearFieldsAction { get; set; }

        public AddBillsViewModel(BillService billService, CustomerService customerService, INavigationService navigationService, Bill selectedBill = null)
        {
            _billService = billService;
            _customerService = customerService;
            _navigationService = navigationService;
            AddBillCommand = new RelayCommand(AddBill, CanAddBill);
            UpdateBillCommand = new RelayCommand(UpdateBill, CanUpdateBill);

            if(selectedBill != null)
            {
                SelectedBill = selectedBill;
                CustomerId = SelectedBill.CustomerId;
                DateIssued = SelectedBill.DateIssued;
                DueDate = SelectedBill.DueDate;
                AmountDue = SelectedBill.AmountDue;
                BillStatus = SelectedBill.BillStatus;
            }
        }

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                _customerId = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateIssued
        {
            get { return _dateIssued; }
            set
            {
                _dateIssued = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        public decimal AmountDue
        {
            get { return _amountDue; }
            set
            {
                _amountDue = value;
                OnPropertyChanged();
            }
        }
        
        public string BillStatus
        {
            get { return _billStatus; }
            set
            {
                _billStatus = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddBillCommand
        {
            get; private set;
        }

        public bool CanAddBill (object parameter)
        {
            return true;  // might want to implement validation here.
        }

        public Bill SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                OnPropertyChanged();
            }
        }

        public System.Action CloseAction { get; set; }

        public void AddBill(object parameter)
        {
            if (_customerService.IsCustomerExist(CustomerId))
            {
                var bill = new Bill { CustomerId = this.CustomerId, DateIssued = this.DateIssued, DueDate = this.DueDate, AmountDue = this.AmountDue, BillStatus = this.BillStatus };
                _billService.InsertBill(bill);
                MessageBox.Show("New bill added successfully!");

                // Invoke the action to clear the fields in the view
                ClearFieldsAction?.Invoke();

                // Close the window after the bill is added
                CloseAction?.Invoke();

                // Navigate back to the MainWindow after adding the bill
                //var mainViewModel = new MainViewModel(_userService, _customerService, _billService, _navigationService);
                //_navigationService.Navigate(mainViewModel);
            }

            else
            {
                MessageBox.Show("The provided Customer ID does not exist. Please check and try again.", "Invalid Customer ID", MessageBoxButton.OK, MessageBoxImage.Warning);               
                ClearFieldsAction?.Invoke();
            }
        }

        public RelayCommand UpdateBillCommand { get; private set; }

        public bool CanUpdateBill(object parameter)
        {
            return SelectedBill != null;
        }

        public void UpdateBill(object parameter)
        {
            if (SelectedBill != null)
            {
                var billToUpdate = _billService.FetchBillById(SelectedBill.BillId);

                billToUpdate.CustomerId = this.CustomerId;
                billToUpdate.DateIssued = this.DateIssued;
                billToUpdate.DueDate = this.DueDate;
                billToUpdate.AmountDue = this.AmountDue;
                billToUpdate.BillStatus = this.BillStatus;

                _billService.UpdateBill(billToUpdate);
                MessageBox.Show("Bill updated successfully!");

                ClearFieldsAction?.Invoke();
            }
            else
            {
                MessageBox.Show("No bill selected. Please select a bill and try again.");
            }
        }
    }
}
