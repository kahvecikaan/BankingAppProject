﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingApp.Domain;

namespace BankingApp.DAL
{
    public class CustomerData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BankingAppConnectionString"].ConnectionString;

        public void InsertCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.InsertCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass parameters to the stored procedure
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@AccountType", customer.AccountType);
                    command.Parameters.AddWithValue("@Balance", customer.Balance);

                    SqlParameter outputIdParam = new SqlParameter("@NewCustomerId", SqlDbType.Int);
                    outputIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputIdParam);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Get the ID of the new customer from the output parameter
                    customer.CustomerId = (int)outputIdParam.Value;

                    // Check the return value of the stored procedure
                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the InsertCustomer procedure in the database: {errorMessageParam.Value}");
                    }
                }
            }
        }
        
        public bool UpdateCustomer(Customer customer)
        {
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.UpdateCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@AccountType", customer.AccountType);
                    command.Parameters.AddWithValue("@Balance", customer.Balance);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();
                    command.ExecuteNonQuery();

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the UpdateCustomer procedure in the database: {errorMessageParam.Value}");
                    }
                    return true;
                }
            }
        }

        public Customer FetchCustomer(int customerId)
        {
            Customer customer = null;
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.FetchCustomer", connection))
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
                        if(reader.Read())
                        {
                            customer = new Customer();
                            customer.CustomerId = (int)reader["CustomerId"];
                            customer.FirstName = reader["FirstName"].ToString();
                            customer.LastName = reader["LastName"].ToString();
                            customer.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            customer.Address = reader["Address"].ToString();
                            customer.PhoneNumber = reader["PhoneNumber"].ToString();
                            customer.AccountType = (int)reader["AccountType"];
                            customer.Balance = (decimal)reader["Balance"];
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchCustomer procedure in the database: {errorMessageParam.Value}");
                    }
                    return customer;
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.DeleteCustomer", connection))
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
                    command.ExecuteNonQuery();

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the DeleteCustomer procedure in the database: {errorMessageParam.Value}");
                    }
                }
            }
        }

        public List<CustomerDetails> FetchAllCustomerDetails()
        {
            var customerDetailsList = new List<CustomerDetails>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchAllCustomerDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
                            var customerDetails = new CustomerDetails();
                            customerDetails.CustomerId = (int)reader["CustomerId"];
                            customerDetails.FirstName = reader["FirstName"].ToString();
                            customerDetails.LastName = reader["LastName"].ToString();
                            customerDetails.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            customerDetails.Email = reader["Email"].ToString();
                            customerDetails.Address = reader["Address"].ToString();
                            customerDetails.PhoneNumber = reader["PhoneNumber"].ToString();
                            customerDetails.AccountTypeDescription = reader["AccountTypeDescription"].ToString();
                            customerDetails.Balance = (decimal)reader["Balance"];
                            customerDetailsList.Add(customerDetails);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An exception occurred during the FetchAllCustomerDetails procedure in the database: {errorMessageParam.Value}");
                    }

                    return customerDetailsList;
                }
            }
        }

        public List<CustomerDetails> SearchCustomerDetails(CustomerFilter filter)
        {
            var customerDetailsList = new List<CustomerDetails>();
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.SearchCustomerDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerId", (object)filter.CustomerId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FirstName", (object)filter.FirstName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", (object)filter.LastName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AccountType", (object)filter.AccountType ?? DBNull.Value);

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
                            var customerDetails = new CustomerDetails();
                            customerDetails.CustomerId = (int)reader["CustomerId"];
                            customerDetails.FirstName = reader["FirstName"].ToString();
                            customerDetails.LastName = reader["LastName"].ToString();
                            customerDetails.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            customerDetails.Email = reader["Email"].ToString();
                            customerDetails.Address = reader["Address"].ToString();
                            customerDetails.PhoneNumber = reader["PhoneNumber"].ToString();
                            customerDetails.AccountTypeDescription = reader["AccountTypeDescription"].ToString();
                            customerDetails.Balance = (decimal)reader["Balance"];
                            customerDetailsList.Add(customerDetails);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An exception occurred during the FetchAllCustomerDetails procedure in the database: {errorMessageParam.Value}");
                    }

                    return customerDetailsList;
                }
            }
        }
    }
}
