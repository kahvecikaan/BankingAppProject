using System.Collections.Generic;
using System.Windows.Input;
using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;

namespace BankingApp.UI.ViewModels
{
    public class SearchCustomersViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;

        private int? _customerId;
        private string _firstName;
        private string _lastName;
        private List<Customer> _customers;

        public SearchCustomersViewModel(CustomerService customerService, INavigationService navigationService)
        {
            _customerService = customerService;
            _navigationService = navigationService;

            SearchCustomersCommand = new RelayCommand(SearchCustomers, CanSearchCustomers);
        }

        public int? CustomerId
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

        public List<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCustomersCommand { get; }

        private bool CanSearchCustomers(object parameter)
        {
            return true;  // might want to implement validation here.
        }

        private void SearchCustomers(object parameter)
        {
            Customers = _customerService.SearchCustomers(CustomerId, FirstName, LastName);
        }
    }
}
