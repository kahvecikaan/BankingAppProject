using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Domain
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
