using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using System.Collections.ObjectModel;
using BankingApp.UI.Events;
using BankingApp.Common.Events;

namespace BankingApp.UI.ViewModels
{
    public class ManageCustomersViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly ParameterService _parameterService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private CustomerDetails _selectedCustomer;

        public CustomerDetails SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        private CustomerFilter _customerFilter;
        public CustomerFilter CustomerFilter
        {
            get { return _customerFilter; }
            set
            {
                _customerFilter = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchCustomersCommand { get; set; }
        
        public ObservableCollection<CustomerDetails> Customers { get; set; }

        //public ObservableCollection<string> AccountTypes { get; set; }

        public ObservableCollection<Parameter> AccountTypes { get; set; }

        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand UpdateCustomerCommand { get; set; }
        public RelayCommand NavigateToAddCustomerCommand { get; set; }


        public ManageCustomersViewModel(CustomerService customerService, ParameterService parameterService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _customerService = customerService;
            _parameterService = parameterService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<CustomerUpdatedEvent>(OnCustomerUpdated);

            Customers = new ObservableCollection<CustomerDetails>(_customerService.FetchAllCustomerDetails());

            AccountTypes = new ObservableCollection<Parameter>(_parameterService.FetchParametersByType("AccountType"));

            CustomerFilter = new CustomerFilter();

            DeleteCustomerCommand = new RelayCommand(DeleteCustomer, _ => SelectedCustomer != null);
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer, _ => SelectedCustomer != null);
            NavigateToAddCustomerCommand = new RelayCommand(NavigateToAddCustomer, _ => true);
            SearchCustomersCommand = new RelayCommand(SearchCustomers, _ => true);
        }

        private void DeleteCustomer(object obj)
        {
            _customerService.DeleteCustomer(SelectedCustomer.CustomerId);
            Customers.Remove(SelectedCustomer);
        }

        private void UpdateCustomer(object obj)
        {
            var addCustomersViewModel = new AddCustomersViewModel(_customerService, _parameterService, _navigationService, _eventAggregator, SelectedCustomer.ToCustomer());
            _navigationService.Navigate(addCustomersViewModel);
        }

        private void NavigateToAddCustomer(object obj)
        {
            var addCustomersViewModel = new AddCustomersViewModel(_customerService, _parameterService, _navigationService, _eventAggregator);
            _navigationService.Navigate(addCustomersViewModel);
        }

        private void OnCustomerUpdated(CustomerUpdatedEvent customerUpdatedEvent)
        {
            Customers = new ObservableCollection<CustomerDetails>(_customerService.FetchAllCustomerDetails());
            OnPropertyChanged(nameof(Customers));
        }

        private void SearchCustomers(object obj)
        {
            var customers = _customerService.SearchCustomerDetails(CustomerFilter);
            Customers.Clear();

            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
            OnPropertyChanged(nameof(Customers)); // Notify UI about the changes
        }
    }
}
