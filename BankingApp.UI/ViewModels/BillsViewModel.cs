using BankingApp.BLL;
using BankingApp.Domain;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using System.Collections.ObjectModel;

namespace BankingApp.UI.ViewModels
{
    public class BillsViewModel : BaseViewModel
    {
        private readonly BillService _billService;
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;

        private Bill _selectedBill;
        public Bill SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Bill> Bills { get; set; }

        public RelayCommand DeleteBillCommand { get; set; }
        public RelayCommand UpdateBillCommand { get; set; }
        public RelayCommand NavigateToAddBillCommand { get; set; }

        public BillsViewModel(BillService billService, CustomerService customerService, INavigationService navigationService)
        {
            _billService = billService;
            _customerService = customerService;
            _navigationService = navigationService;

            Bills = new ObservableCollection<Bill>(_billService.FetchAllBills());

            DeleteBillCommand = new RelayCommand(DeleteBill, _ => SelectedBill != null);
            UpdateBillCommand = new RelayCommand(UpdateBill, _ => SelectedBill != null);
            NavigateToAddBillCommand = new RelayCommand(NavigateToAddBill, _ => true);

            _billService.BillAdded += () =>
            {
                Bills = new ObservableCollection<Bill>(_billService.FetchAllBills());
                OnPropertyChanged(nameof(Bills));
            };
        }

        private void DeleteBill(object obj)
        {
            _billService.DeleteBill(SelectedBill.BillId);
            Bills.Remove(SelectedBill);
        }

        private void UpdateBill(object obj)
        {
            var addBillsViewModel = new AddBillsViewModel(_billService, _customerService, _navigationService, SelectedBill);
            _navigationService.Navigate(addBillsViewModel);
        }
        
        private void NavigateToAddBill(object obj)
        {
            var addBillsViewModel = new AddBillsViewModel(_billService, _customerService, _navigationService);
            _navigationService.Navigate(addBillsViewModel);
        }
    }
}
