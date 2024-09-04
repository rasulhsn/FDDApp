namespace WebApp.Core.Accounts.EventModels
{
    public class MoneyDepositedEventModel
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
