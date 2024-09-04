namespace WebApp.Core.Accounts.EventModels
{
    public class MoneyWithdrawnEventModel
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
