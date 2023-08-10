using System;

namespace BankingApp.Domain
{
    public class CustomerDetails
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountTypeDescription { get; set; }
        public decimal Balance { get; set; }

        public Customer ToCustomer()
        {
            return new Customer
            {
                CustomerId = this.CustomerId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                Address = this.Address,
                PhoneNumber = this.PhoneNumber,
                AccountType = this.AccountTypeDescription == "Savings" ? 1 : 2,
                Balance = this.Balance
            };
        }
    }
}
