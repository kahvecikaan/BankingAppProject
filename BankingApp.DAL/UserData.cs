using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using BankingApp.Domain;
// using PHelper = PasswordUtility.PasswordHelper;

namespace BankingApp.DAL
{
    public class UserData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BankingAppConnectionString"].ConnectionString;

        public User FetchUser(string username, string hashedPassword)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.FetchUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1); // NVARCHAR(MAX), -1 indicates 'max'
                    errorMessageParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(errorMessageParam);

                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnValue);

                    connection.Open();
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User();
                            user.UserId = (int)reader["UserId"];
                            user.Username = reader["Username"].ToString();
                            user.Password = reader["Password"].ToString();
                        }
                    }

                    if ((int)returnValue.Value == -1)
                    {
                        throw new Exception($"An error occurred during the FetchUser procedure in the database: {errorMessageParam.Value}");
                    }
                    return user;
                }
            }
        }

        public void UpdateUserPassword(int userId, string hashedPassword)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.UpdateUserPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("UserId", userId);
                    command.Parameters.AddWithValue("Password", hashedPassword);

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
                        throw new Exception($"An error occurred during the UpdateUserpassword procedure in the database: {errorMessageParam.Value}");
                    }
                }
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>(); 
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using(SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            User user = new User();
                            user.UserId = (int)reader["UserId"];
                            user.Username = reader["Username"].ToString();
                            user.Password = reader["Password"].ToString();
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
    }
}
