using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingApp.Domain;


namespace BankingApp.DAL
{
    public class ActionData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BankingAppConnectionString"].ConnectionString;

        public void InsertAction(Domain.Action action)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.InsertAction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", action.UserId);
                    command.Parameters.AddWithValue("@CustomerId", action.CustomerId);
                    command.Parameters.AddWithValue("@ActionType", action.ActionType);
                    command.Parameters.AddWithValue("@Details", action.Details);


                    SqlParameter outputIdParam = new SqlParameter("@NewActionId", SqlDbType.Int);
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

                    int newActionId = (int)outputIdParam.Value;

                    if((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the InsertAction procedure in the database: {errorMessageParam.Value}");
                    }
                }
            }
        }

        public List<Domain.Action> FetchActionsByUser(int userId)
        {
            var actions = new List<Domain.Action>();

            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.FetchActionsByUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", userId);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);


                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var action = new Domain.Action();
                            action.ActionId = (int)reader["ActionId"];
                            action.UserId = (int)reader["UserId"];
                            action.CustomerId = (int)reader["CustomerId"];
                            action.ActionType = reader["ActionType"].ToString();
                            action.ActionDate = (DateTime)reader["ActionDate"];
                            action.Details = reader["Details"].ToString();
                            actions.Add(action);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchActionsByUser procedure in the database: {errorMessageParam.Value}");
                    }

                    return actions;
                }
            }
        }

        public List<Domain.Action> FetchActionsByCustomer(int customerId)
        {
            var actions = new List<Domain.Action>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchActionsByCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);


                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var action = new Domain.Action();
                            action.ActionId = (int)reader["ActionId"];
                            action.UserId = (int)reader["UserId"];
                            action.CustomerId = (int)reader["CustomerId"];
                            action.ActionType = reader["ActionType"].ToString();
                            action.ActionDate = (DateTime)reader["ActionDate"];
                            action.Details = reader["Details"].ToString();
                            actions.Add(action);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchActionsByCustomer procedure in the database: {errorMessageParam.Value}");
                    }

                    return actions;
                }
            }
        }
    }
}
