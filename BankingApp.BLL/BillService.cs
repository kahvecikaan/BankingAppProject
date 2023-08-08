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

        // public event System.Action BillAdded = delegate { };

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
            // BillAdded.Invoke();
        }

        public bool UpdateBill(Bill bill)
        {
            try
            {
                if (!_customerService.IsCustomerExist(bill.CustomerId))
                {
                    throw new Exception($"Customer with {bill.CustomerId} does not exist!");
                }
                return this._billData.UpdateBill(bill);
            }
            catch(DatabaseException ex)
            {
                throw new BusinessException("Failed to update bill: " + ex.Message);
            }
        }

        public List<Bill> FetchBillsByCustomer(int customerId)
        {
            return this._billData.FetchBillsByCustomer(customerId);
        }

        public void DeleteBill(int billId)
        {
            this._billData.DeleteBill(billId);
        }

        public List<Bill> FetchAllBills()
        {
            return this._billData.FetchAllBills();
        }

        public Bill FetchBillById(int billId)
        {
            return this._billData.FetchBillById(billId);
        }

        public List<BillDetails> FetchAllBillDetails()
        {
            return this._billData.FetchAllBillDetails();
        }

        public List<BillDetails> SearchBillDetails(BillFilter filter)
        {
            return this._billData.SearchBillDetails(filter);
        }
    }
}
