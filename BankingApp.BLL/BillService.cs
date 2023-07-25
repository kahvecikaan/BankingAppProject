using System;
using System.Collections.Generic;
using BankingApp.DAL;
using BankingApp.Domain;

namespace BankingApp.BLL
{
    public class BillService
    {
        private readonly BillData _billData;
        private readonly CustomerService _customerService;

        public BillService(BillData billData, CustomerService customerService)
        {
            this._billData = billData;
            this._customerService = customerService;
        }

        public void InsertBill(Bill bill)
        {
            if(!_customerService.IsCustomerExist(bill.CustomerId))
            {
                throw new Exception($"Customer with {bill.CustomerId} does not exist!");
            }
            this._billData.InsertBill(bill);
        }

        public bool UpdateBill(Bill bill)
        {
            if (!_customerService.IsCustomerExist(bill.CustomerId))
            {
                throw new Exception($"Customer with {bill.CustomerId} does not exist!");
            }
            return this._billData.UpdateBill(bill);
        }

        public List<Bill> FetchBillsByCustomer(int customerId)
        {
            return this._billData.FetchBillsByCustomer(customerId);
        }

        public void DeleteBill(int billId)
        {
            this._billData.DeleteBill(billId);
        }
    }
}
