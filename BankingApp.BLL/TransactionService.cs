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
