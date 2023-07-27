using System;
using System.Windows;
using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using BankingApp.UI.Events;

namespace BankingApp.UI.ViewModels
{
    public class AddBillsViewModel : BaseViewModel
    {
        private readonly BillService _billService;
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        // private readonly UserService _userService;
        private int _customerId;
        private DateTime _dateIssued;
        private DateTime _dueDate;
        private decimal _amountDue;
        private string _billStatus;
        //private Bill _selectedBill;
        private Bill _editingBill;
        

        public System.Action ClearFieldsAction { get; set; }
        public System.Action CloseAction { get; set; }
        public RelayCommand SaveChangesCommand { get; }


        public AddBillsViewModel(BillService billService, CustomerService customerService, INavigationService navigationService, IEventAggregator eventAggregator, Bill editingBill = null)
        {
            _billService = billService;
            _customerService = customerService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            _editingBill = editingBill;
            OnPropertyChanged(nameof(ButtonText)); // notify View about ButtonText changes

            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);

            //AddBillCommand = new RelayCommand(AddBill, CanAddBill);
            //UpdateBillCommand = new RelayCommand(UpdateBill, CanUpdateBill);

            if(_editingBill != null)
            {
                CustomerId = _editingBill.CustomerId;
                DateIssued = _editingBill.DateIssued;
                DueDate = _editingBill.DueDate;
                AmountDue = _editingBill.AmountDue;
                BillStatus = _editingBill.BillStatus;
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

        public string ButtonText => _editingBill != null ? "Update Bill" : "Add Bill";

        private bool CanSaveChanges(object parameter)
        {
            return true;
        }

        public void SaveChanges(object parameter)
        {
            Bill bill = null;

            if (_editingBill != null)
            {
                // editing an existing bill, update its properties
                _editingBill.CustomerId = this.CustomerId;
                _editingBill.DateIssued = this.DateIssued;
                _editingBill.DueDate = this.DueDate;
                _editingBill.AmountDue = this.AmountDue;
                _editingBill.BillStatus = this.BillStatus;
                _billService.UpdateBill(_editingBill);

                bill = _editingBill;

                // _eventAggregator.Publish(new BillUpdatedEvent { UpdatedBill = _editingBill });

                MessageBox.Show("Bill updated successfully");
            }
            else
            {
                // creating a new bill, validate the customer first
                if (_customerService.IsCustomerExist(CustomerId))
                {
                    var newBill = new Bill
                    {
                        CustomerId = this.CustomerId,
                        DateIssued = this.DateIssued,
                        DueDate = this.DueDate,
                        AmountDue = this.AmountDue,
                        BillStatus = this.BillStatus                       
                    };

                    _billService.InsertBill(newBill);
                    bill = newBill; // store the reference to the new bill

                    MessageBox.Show("New bill added successfully!");
                }
                else
                {
                    MessageBox.Show("The provied CustomerId does not exist. Please check and try again", "Invalid CustomerID", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
            if (bill != null)
            {
                // publish the event to notify other parts of the app about the change
                _eventAggregator.Publish(new BillUpdatedEvent { UpdatedBill = bill });
            }
            
            // clear fields and close the window after saving changes
            ClearFieldsAction?.Invoke();
            CloseAction?.Invoke();
        }
    }
}
