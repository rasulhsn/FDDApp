namespace WebApp.Core.Accounts.EventModels
{
    public class MoneyDepositedEventModel : IEventModel<AccountModel>
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public void Apply(AccountModel accountModel)
        {
            accountModel.Balance += this.Amount;
        }
    }
}
