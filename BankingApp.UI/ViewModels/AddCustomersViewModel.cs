using System;
using System.Windows;
using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using BankingApp.UI.Events;

namespace BankingApp.UI.ViewModels
{
    public class AddCustomersViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private int _customerId;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _email;
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private string _accountType;
        private Customer _editingCustomer;

        public System.Action ClearFieldsAction { get; set; }
        public System.Action CloseAction { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }

        public AddCustomersViewModel(CustomerService customerService, INavigationService navigationService, IEventAggregator eventAggregator, Customer editingCustomer = null)
        {
            _customerService = customerService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _editingCustomer = editingCustomer;

            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            if(_editingCustomer != null)
            {
                _customerId = _editingCustomer.CustomerId;
                _firstName = _editingCustomer.FirstName;
                _lastName = _editingCustomer.LastName;
                _address = _editingCustomer.Address;
                _dateOfBirth = _editingCustomer.DateOfBirth;
                _email = _editingCustomer.Email;
                _phoneNumber = _editingCustomer.PhoneNumber;
                _accountType = _editingCustomer.AccountType;
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

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (_editingCustomer == null)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string AccountType
        {
            get { return _accountType; }
            set
            {
                _accountType = value;
                OnPropertyChanged();
            }
        }

        public bool CanEditDateOfBirth => _editingCustomer == null;
        public string ButtonText => _editingCustomer != null ? "Update Customer" : "Add Customer";

        private bool CanSaveChanges(object parameter)
        {
            return true;
        }
        private void SaveChanges(object parameter)
        {
            Customer customer = null;

            if(_editingCustomer != null)
            {
                // Editing an existing customer, update its properties
                _editingCustomer.FirstName = this.FirstName;
                _editingCustomer.LastName = this.LastName;
                _editingCustomer.Email = this.Email;
                _editingCustomer.Address = this.Address;
                _editingCustomer.AccountType = this.AccountType;
                _editingCustomer.PhoneNumber = this.PhoneNumber;
                _customerService.UpdateCustomer(_editingCustomer);

                customer = _editingCustomer;

                MessageBox.Show("Customer updated successfully!");
            }
            else
            {
                // Creating a new customer
                var newCustomer = new Customer
                {
                    CustomerId = this.CustomerId,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    DateOfBirth = this.DateOfBirth,
                    Email = this.Email,
                    Address = this.Address,
                    PhoneNumber = this.PhoneNumber,
                    AccountType = this.AccountType
                };

                _customerService.InsertCustomer(newCustomer);
                customer = newCustomer;

                MessageBox.Show("New customer added successfully!");
            }

            if (customer != null)
            {
                // publish the event to notify other parts of the app about the change
                _eventAggregator.Publish(new CustomerUpdatedEvent { UpdatedCustomer = customer });
            }


            // Clear fields and close the window after saving changes
            ClearFieldsAction?.Invoke();
            CloseAction?.Invoke();
        }
    }
}
