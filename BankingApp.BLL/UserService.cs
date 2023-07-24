using System.Collections.Generic;
using BankingApp.DAL;
using PasswordUtility;
using BankingApp.Domain;

namespace BankingApp.BLL
{
    public class UserService
    {
        private UserData _userData;

        public UserService()
        {

        }

        public UserService(UserData userData)
        {
            this._userData = userData;
        }
        public void UpdateUserPassword(int userId, string plainTextPassword)
        {
            string hashedPassword = PasswordHelper.HashPassword(plainTextPassword);
            _userData.UpdateUserPassword(userId, hashedPassword);
        }

        public User FetchUser(string username, string plainTextPassword)
        {
            string hashedPassword = PasswordHelper.HashPassword(plainTextPassword);
            return _userData.FetchUser(username, hashedPassword);
        }

        public void ConvertAllPasswordsToHashed()
        {
            List<User> users = _userData.GetAllUsers();
            foreach(var user in users)
            {
                string hashedPassword = PasswordHelper.HashPassword(user.Password);
                _userData.UpdateUserPassword(user.UserId, hashedPassword);
            }
        }
    }
}
