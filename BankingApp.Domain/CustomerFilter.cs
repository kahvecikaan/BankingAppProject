namespace BankingApp.Domain
{
    public class CustomerFilter
    {
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AccountType { get; set; }
    }
}
