using System;
using System.Windows;
using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using BankingApp.UI.Events;
using System.Linq;
using System.Collections.ObjectModel;
using BankingApp.Common.Events;

namespace BankingApp.UI.ViewModels
{
    public class AddBillsViewModel : BaseViewModel
    {
        private readonly BillService _billService;
        private readonly CustomerService _customerService;
        private readonly ParameterService _parameterService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private int _customerId;
        private DateTime _dateIssued;
        private DateTime _dueDate;
        private decimal _amountDue;
        private BillStatusItem _billStatus;
        private Bill _editingBill;

        public System.Action ClearFieldsAction { get; set; }
        public System.Action CloseAction { get; set; }
        public RelayCommand SaveChangesCommand { get; }

        public AddBillsViewModel(BillService billService, CustomerService customerService, ParameterService parameterService, INavigationService navigationService, IEventAggregator eventAggregator, Bill editingBill = null)
        {
            _billService = billService;
            _customerService = customerService;
            _parameterService = parameterService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            _editingBill = editingBill;
            OnPropertyChanged(nameof(ButtonText));

            var parameters = _parameterService.FetchParametersByType("BillStatus");
            BillStatusParameters = new ObservableCollection<BillStatusItem>(
                parameters.Select(p => new BillStatusItem { Code = p.Code, Description = p.Description }));

            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);

            if (_editingBill != null)
            {
                CustomerId = _editingBill.CustomerId;
                DateIssued = _editingBill.DateIssued;
                DueDate = _editingBill.DueDate;
                AmountDue = _editingBill.AmountDue;
                BillStatus = BillStatusParameters.FirstOrDefault(bs => bs.Code == _editingBill.BillStatus);
            }
            else
            {
                _dateIssued = DateTime.Today;
                _dueDate = DateTime.Today;
                BillStatus = BillStatusParameters.First();
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

        public BillStatusItem BillStatus
        {
            get { return _billStatus; }
            set
            {
                _billStatus = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BillStatusItem> BillStatusParameters { get; set; }

        public bool CanEditCustomerId => _editingBill == null;
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
                _editingBill.CustomerId = this.CustomerId;
                _editingBill.DateIssued = this.DateIssued;
                _editingBill.DueDate = this.DueDate;
                _editingBill.AmountDue = this.AmountDue;
                _editingBill.BillStatus = BillStatus.Code;

                _billService.UpdateBill(_editingBill);
                bill = _editingBill;

                MessageBox.Show("Bill updated successfully");
            }
            else
            {
                if (_customerService.IsCustomerExist(CustomerId))
                {
                    var newBill = new Bill
                    {
                        CustomerId = this.CustomerId,
                        DateIssued = this.DateIssued,
                        DueDate = this.DueDate,
                        AmountDue = this.AmountDue,
                        BillStatus = BillStatus.Code
                    };

                    _billService.InsertBill(newBill);
                    bill = newBill;

                    MessageBox.Show("New bill added successfully!");
                }
                else
                {
                    MessageBox.Show("The provided CustomerId does not exist. Please check and try again", "Invalid CustomerID", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (bill != null)
            {
                _eventAggregator.Publish(new BillUpdatedEvent { UpdatedBill = bill });
            }

            if(bill != null && BillStatus.Code == 2)
            {
                _eventAggregator.Publish(new Common.BillPaidEvent { PaidBill = bill });
            }

            ClearFieldsAction?.Invoke();
            CloseAction?.Invoke();
        }
    }
}
