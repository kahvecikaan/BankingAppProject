using System;
using System.Collections.Generic;
using BankingApp.DAL;
using BankingApp.Domain;

namespace BankingApp.BLL
{
    public class TransactionService
    {
        private readonly TransactionData _transactionData;

        public TransactionService(TransactionData transactionData)
        {
            this._transactionData = transactionData;
        }

        public void InsertTransaction(Transaction transaction)
        {
            this._transactionData.InsertTransaction(transaction);
        }

        public List<Transaction> FetchTransactionsByCustomer(int customerId)
        {
            return this._transactionData.FetchTransactionsByCustomer(customerId);
        }

        public List<Transaction> FetchTransactionByDateRange(DateTime startDate, DateTime endDate)
        {
            return this._transactionData.FetchTransactionsByDateRange(startDate, endDate);
        }
        
        public List<Transaction> FetchTransactionByUser(int userId)
        {
            return this._transactionData.FetchTransactionsByUser(userId);
        }
        
        public List<TransactionDetails> FetchAllTransactionDetails()
        {
            return this._transactionData.FetchAllTransactionDetails();
        }

        public List<TransactionDetails> SearchTransactionDetails(TransactionFilter filter)
        {
            return this._transactionData.SearchTransactionDetails(filter);
        }
    }
}
