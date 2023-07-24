using System;
using System.Collections.Generic;
using BankingApp.DAL;
using BankingApp.Domain;

namespace BankingApp.BLL
{
    public class BillService
    {
        private readonly BillData _billData;

        public BillService(BillData billData)
        {
            this._billData = billData;
        }

        public void InsertBill(Bill bill)
        {
            this._billData.InsertBill(bill);
        }

        public bool UpdateBill(Bill bill)
        {
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
