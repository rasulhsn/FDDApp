namespace AccountsWebApp.Core.Accounts.EventModels
{
    public class MoneyWithdrawn
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
