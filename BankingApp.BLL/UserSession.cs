using BankingApp.Domain;

namespace BankingApp.BLL
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
    }
}
