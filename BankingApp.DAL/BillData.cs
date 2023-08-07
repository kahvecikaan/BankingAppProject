using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using BankingApp.Domain;

namespace BankingApp.DAL
{
    public class BillData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BankingAppConnectionString"].ConnectionString;

        public void InsertBill(Bill bill)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.InsertBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerId", bill.CustomerId);
                    command.Parameters.AddWithValue("@DateIssued", bill.DateIssued);
                    command.Parameters.AddWithValue("@DueDate", bill.DueDate);
                    command.Parameters.AddWithValue("@AmountDue", bill.AmountDue);
                    command.Parameters.AddWithValue("@BillStatus", bill.BillStatus);



                    SqlParameter outputIdParam = new SqlParameter("@NewBillId", SqlDbType.Int);
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

                    // bill.BillId = (int)outputIdParam.Value;
                    if (outputIdParam.Value != DBNull.Value)
                        bill.BillId = (int)outputIdParam.Value;

                    if ((int)returnValue.Value == -1)
                        throw new Exception($"An error occurred during the InsertBill procedure in the database: {errorMessageParam.Value}");
                }
            }
        }

        public bool UpdateBill(Bill bill)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.UpdateBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BillId", bill.BillId);
                    command.Parameters.AddWithValue("@CustomerId", bill.CustomerId);
                    command.Parameters.AddWithValue("@DateIssued", bill.DateIssued);
                    command.Parameters.AddWithValue("@DueDate", bill.DueDate);
                    command.Parameters.AddWithValue("@AmountDue", bill.AmountDue);
                    command.Parameters.AddWithValue("@BillStatus", bill.BillStatus);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();
                    command.ExecuteNonQuery();

                    if ((int)returnValue.Value == -1)
                        throw new Exception($"An error occurred during the UpdateBill procedure in the database: {errorMessageParam.Value}");
                    return true;
                }
            }
        }

        public List<Bill> FetchBillsByCustomer(int customerId)
        {
            var bills = new List<Bill>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchBillsByCustomer", connection))
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
                        while (reader.Read())
                        {
                            var bill = new Bill();
                            bill.BillId = (int)reader["BillId"];
                            bill.CustomerId = (int)reader["CustomerId"];
                            bill.DateIssued = (DateTime)reader["DateIssued"];
                            bill.DueDate = (DateTime)reader["DueDate"];
                            bill.AmountDue = (decimal)reader["AmountDue"];
                            bill.BillStatus = (int)reader["BillStatus"];
                            bills.Add(bill);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                        throw new Exception($"An error occurred during the FetchBillsByCustomer procedure in the database: {errorMessageParam.Value}");

                    return bills;
                }
            }
        }

        public void DeleteBill(int billId)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.DeleteBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BillId", billId);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();
                    command.ExecuteNonQuery();

                    if ((int)returnValue.Value == -1)
                        throw new Exception($"An error occurred during the DeleteBill procedure in the database: {errorMessageParam.Value}");
                }
            }
        }

        public List<Bill> FetchAllBills()
        {
            var bills = new List<Bill>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchAllBills", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bill = new Bill();
                            bill.BillId = (int)reader["BillId"];
                            bill.CustomerId = (int)reader["CustomerId"];
                            bill.DateIssued = (DateTime)reader["DateIssued"];
                            bill.DueDate = (DateTime)reader["DueDate"];
                            bill.AmountDue = (decimal)reader["AmountDue"];
                            bill.BillStatus = (int)reader["BillStatus"];
                            bills.Add(bill);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchAllBills procedure in the database: {errorMessageParam.Value}");
                    }
                    return bills;
                }
            }
        }

        public Bill FetchBillById(int billId)
        {
            Bill bill = null;
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.FetchBillById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BillId", billId);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates that 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;

                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bill = new Bill();
                            bill.BillId = (int)reader["BillId"];
                            bill.CustomerId = (int)reader["CustomerId"];
                            bill.DateIssued = (DateTime)reader["DateIssued"];
                            bill.DueDate = (DateTime)reader["DueDate"];
                            bill.AmountDue = (decimal)reader["AmountDue"];
                            bill.BillStatus = (int)reader["BillStatus"];
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchBillById procedure in the database: {errorMessageParam.Value}");
                    }

                    return bill;
                }
            }
        }

        public List<BillDetails> FetchAllBillDetails()
        {
            var billDetailsList = new List<BillDetails>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchAllBillDetails", connection))
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
                        while (reader.Read())
                        {
                            var billDetails = new BillDetails();
                            billDetails.BillId = (int)reader["BillId"];
                            billDetails.CustomerId = (int)reader["CustomerId"];
                            billDetails.CustomerFirstName = reader["FirstName"].ToString();
                            billDetails.CustomerLastName = reader["LastName"].ToString();
                            billDetails.DateIssued = (DateTime)reader["DateIssued"];
                            billDetails.DueDate = (DateTime)reader["DueDate"];
                            billDetails.AmountDue = (decimal)reader["AmountDue"];
                            billDetails.BillStatusDescription = reader["BillStatusDescription"].ToString();
                            billDetailsList.Add(billDetails);
                        }
                    }

                    if((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An exception occurred during the FetchAllBillDetails procedure in the database: {errorMessageParam.Value}");
                    }

                    return billDetailsList;
                }
            }
        }

        public List<BillDetails> SearchBillDetails(BillFilter filter)
        {
            var billDetailsList = new List<BillDetails>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.SearchBillDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BillId", (object)filter.BillId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerId", (object)filter.CustomerId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DateIssuedFrom", (object)filter.DateIssuedFrom ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DateIssuedTo", (object)filter.DateIssuedTo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DueDateFrom", (object)filter.DueDateFrom ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DueDateTo", (object)filter.DueDateTo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AmountDueFrom", (object)filter.AmountDueFrom ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AmountDueTo", (object)filter.AmountDueTo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FirstName", (object)filter.FirstName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", (object)filter.LastName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@BillStatus", (object)filter.BillStatus ?? DBNull.Value);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1);
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var billDetails = new BillDetails();
                            billDetails.BillId = (int)reader["BillId"];
                            billDetails.CustomerId = (int)reader["CustomerId"];
                            billDetails.CustomerFirstName = reader["FirstName"].ToString();
                            billDetails.CustomerLastName = reader["LastName"].ToString();
                            billDetails.DateIssued = (DateTime)reader["DateIssued"];
                            billDetails.DueDate = (DateTime)reader["DueDate"];
                            billDetails.AmountDue = (decimal)reader["AmountDue"];
                            billDetails.BillStatusDescription = reader["BillStatusDescription"].ToString();
                            billDetailsList.Add(billDetails);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An exception occurred during the SearchBillDetails procedure in the database: {errorMessageParam.Value}");
                    }

                    return billDetailsList;
                }
            }
        }
    }
}

