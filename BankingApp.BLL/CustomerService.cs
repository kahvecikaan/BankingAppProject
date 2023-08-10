using System.Collections.Generic;
using BankingApp.DAL;
using BankingApp.Domain;

namespace BankingApp.BLL
{
    public class CustomerService
    {
        private readonly CustomerData _customerData;

        public CustomerService(CustomerData customerData)
        {
            this._customerData = customerData;
        }

        public void InsertCustomer(Customer customer)
        {
            this._customerData.InsertCustomer(customer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return this._customerData.UpdateCustomer(customer);
        }

        public Customer FetchCustomer(int customerId)
        {
            return this._customerData.FetchCustomer(customerId);
        }

        public void DeleteCustomer(int customerId)
        {
            this._customerData.DeleteCustomer(customerId);
        }

        public bool IsCustomerExist(int customerId)
        {
            return this._customerData.FetchCustomer(customerId) != null;
        }

        public List<CustomerDetails> FetchAllCustomerDetails()
        {
            return this._customerData.FetchAllCustomerDetails();
        }

        public List<CustomerDetails> SearchCustomerDetails(CustomerFilter filter)
        {
            return this._customerData.SearchCustomerDetails(filter);
        }
    }
}
