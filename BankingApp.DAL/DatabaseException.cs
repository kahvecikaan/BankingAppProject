using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.DAL
{
    public class DatabaseException : Exception
    {
        public string ProcedureName { get; set; }
        public string SqlErrorMessage { get; set; }

        public DatabaseException(string message, string procedureName, string sqlErrorMessage) : base(message)
        {
            this.ProcedureName = procedureName;
            this.SqlErrorMessage = sqlErrorMessage;
        }

        public override string ToString()
        {
            return $"An error occurred in the '{ProcedureName}' stored procedure: {Message}. SQL Error Message: {SqlErrorMessage}";
        }
    }
}
