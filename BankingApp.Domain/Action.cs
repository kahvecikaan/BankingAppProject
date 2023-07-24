using System;

namespace BankingApp.Domain
{
    public class Action
    {
        public int ActionId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionDate { get; set; }
        public string Details { get; set; }
    }
}
