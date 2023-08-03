using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingApp.Domain;

namespace BankingApp.DAL
{
    public class ParameterData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BankingAppConnectionString"].ConnectionString;

        public List<Parameter> FetchParametersByType(string type)
        {
            var parametersList = new List<Parameter>();
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("dbo.FetchParametersByType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("Type", type);

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
                            var parameter = new Parameter();
                            parameter.Type = reader["Type"].ToString();
                            parameter.Code = (int)reader["Code"];
                            parameter.Description = reader["Description"].ToString();
                            parameter.Active = (bool)reader["Active"];
                            parametersList.Add(parameter);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchParametersByType procedure in the database: {errorMessageParam.Value}");
                    }

                    return parametersList;
                }
            }
        }

        public List<string> FetchAllAccountTypes()
        {
            var accountTypeList = new List<string>();
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchAllAccountTypes", connection))
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
                            var accountType = reader["Description"].ToString();
                            accountTypeList.Add(accountType);
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchParametersByType procedure in the database: {errorMessageParam.Value}");
                    }

                    return accountTypeList;
                }
            }
        }

        public int GetParameterIdByValue(string type, string description)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetParameterIdByValue", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Description", description);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1);
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    int code = (int)command.ExecuteScalar();

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the GetParameterIdByValue procedure in the database: {errorMessageParam.Value}");
                    }

                    return code;
                }
            }
        }

        public string GetParameterValueById(string type, int code)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetParameterValueById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Code", code);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1);
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();

                    string description = command.ExecuteScalar() as string;

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the GetParameterValueById procedure in the database: {errorMessageParam.Value}");
                    }

                    return description;
                }
            }
        }

    }
}
