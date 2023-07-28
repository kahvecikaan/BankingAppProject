using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using System.Collections.ObjectModel;
using BankingApp.UI.Events;

namespace BankingApp.UI.ViewModels
{
    public class BillsViewModel : BaseViewModel
    {
        private readonly BillService _billService;
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private BillDetails _selectedBill;
        public BillDetails SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BillDetails> Bills { get; set; }

        public RelayCommand DeleteBillCommand { get; set; }
        public RelayCommand UpdateBillCommand { get; set; }
        public RelayCommand NavigateToAddBillCommand { get; set; }

        public BillsViewModel(BillService billService, CustomerService customerService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _billService = billService;
            _customerService = customerService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<BillUpdatedEvent>(OnBillUpdated);

            Bills = new ObservableCollection<BillDetails>(_billService.FetchAllBillDetails());

            DeleteBillCommand = new RelayCommand(DeleteBill, _ => SelectedBill != null);
            UpdateBillCommand = new RelayCommand(UpdateBill, _ => SelectedBill != null);
            NavigateToAddBillCommand = new RelayCommand(NavigateToAddBill, _ => true);
        }

        private void DeleteBill(object obj)
        {
            _billService.DeleteBill(SelectedBill.BillId);
            Bills.Remove(SelectedBill);
        }

        private void UpdateBill(object obj)
        {
            var addBillsViewModel = new AddBillsViewModel(_billService, _customerService, _navigationService, _eventAggregator, SelectedBill.ToBill());
            _navigationService.Navigate(addBillsViewModel);
        }
        
        private void NavigateToAddBill(object obj)
        {
            var addBillsViewModel = new AddBillsViewModel(_billService, _customerService, _navigationService, _eventAggregator);
            _navigationService.Navigate(addBillsViewModel);
        }

        private void OnBillUpdated(BillUpdatedEvent billUpdatedEvent)
        {
            Bills = new ObservableCollection<BillDetails>(_billService.FetchAllBillDetails());
            OnPropertyChanged(nameof(Bills));
        }
    }
}
