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

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        transaction.TransactionId = (int)outputIdParam.Value;
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the InsertTransaction procedure in the database: {errorMessageParam.Value}");
                    }
                }
            }
        }

        public List<TransactionDetails> FetchAllTransactionDetails()
        {
            var transactionDetailsList = new List<TransactionDetails>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchAllTransactionDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1);
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
                            var transactionDetails = new TransactionDetails();
                            transactionDetails.TransactionId = (int)reader["TransactionId"];
                            transactionDetails.UserId = (int)reader["UserId"];
                            transactionDetails.CustomerId = (int)reader["CustomerId"];
                            transactionDetails.CustomerFirstName = reader["FirstName"].ToString();
                            transactionDetails.CustomerLastName = reader["LastName"].ToString();
                            transactionDetails.TransactionTypeDescription = reader["TransactionTypeDescription"].ToString();
                            transactionDetails.TransactionAmount = (decimal)reader["TransactionAmount"];
                            transactionDetails.TransactionDate = (DateTime)reader["TransactionDate"];
                            transactionDetailsList.Add(transactionDetails);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An exception occurred during the FetchAllTransactionDetails procedure in the database: {errorMessageParam.Value}");
                    }

                    return transactionDetailsList;
                }
            }
        }

        public List<TransactionDetails> SearchTransactionDetails(TransactionFilter filter)
        {
            var transactionDetailsList = new List<TransactionDetails>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.SearchTransactionDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TransactionId", (object)filter.TransactionId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UserId", (object)filter.UserId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerId", (object)filter.CustomerId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FirstName", (object)filter.CustomerFirstName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", (object)filter.CustomerLastName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TransactionType", (object)filter.TransactionType ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TransactionAmountFrom", (object)filter.TransactionAmountFrom ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TransactionAmountTo", (object)filter.TransactionAmountTo ?? DBNull.Value);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1);
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
                            var transactionDetails = new TransactionDetails();
                            transactionDetails.TransactionId = (int)reader["TransactionId"];
                            transactionDetails.UserId = (int)reader["UserId"];
                            transactionDetails.CustomerId = (int)reader["CustomerId"];
                            transactionDetails.CustomerFirstName = reader["FirstName"].ToString();
                            transactionDetails.CustomerLastName = reader["LastName"].ToString();
                            transactionDetails.TransactionTypeDescription = reader["TransactionTypeDescription"].ToString();
                            transactionDetails.TransactionAmount = (decimal)reader["TransactionAmount"];
                            transactionDetails.TransactionDate = (DateTime)reader["TransactionDate"];
                            transactionDetailsList.Add(transactionDetails);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An exception occurred during the SearchTransactionDetails procedure in the database: {errorMessageParam.Value}");
                    }

                    return transactionDetailsList;
                }
            }
        }
    }
}
