using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingApp.Domain;


namespace BankingApp.DAL
{
    public class TransactionData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BankingAppConnectionString"].ConnectionString;
        public void InsertTransaction(Transaction transaction)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.InsertTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", transaction.UserId);
                    command.Parameters.AddWithValue("@CustomerId", transaction.CustomerId);
                    command.Parameters.AddWithValue("@TransactionAmount", transaction.TransactionAmount);
                    command.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);

                    SqlParameter outputIdParam = new SqlParameter("@NewTransactionId", SqlDbType.Int);
                    outputIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputIdParam);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);


                    connection.Open();
                    command.ExecuteNonQuery();

                    transaction.TransactionId = (int)outputIdParam.Value;

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the InsertTransaction procedure in the database: {errorMessageParam.Value}");
                    }
                }
            }
        }

       public List<Transaction>  FetchTransactionsByCustomer(int customerId)
        {
            var transactions = new List<Transaction>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchTransactionsByCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var transaction = new Transaction();
                            transaction.TransactionId = (int)reader["TransactionId"];
                            transaction.UserId = (int)reader["UserId"];
                            transaction.CustomerId = (int)reader["CustomerId"];
                            transaction.TransactionAmount = (decimal)reader["TransactionAmont"];
                            transaction.TransactionType = reader["TransactionType"].ToString();
                            transaction.TransactionDate = (DateTime)reader["TransactionDate"];
                            transactions.Add(transaction);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchTransactionByCustomer procedure in the database: {errorMessageParam.Value}");
                    }
                    return transactions; // NOTE: Do not forget to handle the case where the customer do not have any transaction and the list is empty
                }
            }
        }

        public List<Transaction> FetchTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            var transactions = new List<Transaction>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchTransactionsByDateRange", connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var transaction = new Transaction();
                            transaction.TransactionId = (int)reader["TransactionId"];
                            transaction.UserId = (int)reader["UserId"];
                            transaction.CustomerId = (int)reader["CustomerId"];
                            transaction.TransactionAmount = (decimal)reader["TransactionAmont"];
                            transaction.TransactionType = reader["TransactionType"].ToString();
                            transaction.TransactionDate = (DateTime)reader["TransactionDate"];
                            transactions.Add(transaction);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchTransactionByDateRange procedure in the database: {errorMessageParam.Value}");
                    }
                    return transactions;
                }
            }
        }

        public List<Transaction> FetchTransactionsByUser(int userId)
        {
            var transactions = new List<Transaction>();
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.FetchTransactionsByUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", userId);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var transaction = new Transaction();
                            transaction.TransactionId = (int)reader["TransactionId"];
                            transaction.UserId = (int)reader["UserId"];
                            transaction.CustomerId = (int)reader["CustomerId"];
                            transaction.TransactionAmount = (decimal)reader["TransactionAmont"];
                            transaction.TransactionType = reader["TransactionType"].ToString();
                            transaction.TransactionDate = (DateTime)reader["TransactionDate"];
                            transactions.Add(transaction);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchTransactionsByCustomer procedure in the database: {errorMessageParam.Value}");
                    }
                    return transactions;
                }
            }
        }
    }
}
