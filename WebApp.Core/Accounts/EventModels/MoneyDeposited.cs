namespace WebApp.Core.Accounts.EventModels
{
    public class MoneyDeposited
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
