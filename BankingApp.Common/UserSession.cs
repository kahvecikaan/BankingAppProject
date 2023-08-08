using BankingApp.Domain;

namespace BankingApp.Common
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
    }
}
