namespace WebApp.Core.Accounts.EventModels
{
    public class AccountCreatedEventModel : IEventModel<AccountModel>
    {
        public Guid AccountId { get; set; }
        public string Owner { get; set; }
        public decimal InitialBalance { get; set; }
        public AccountStatus Status { get; set; }

        public bool IsDeleted = false;

        public void Apply(AccountModel accountModel)
        {
            accountModel.Id = this.AccountId;
            accountModel.Owner = this.Owner;
            accountModel.Balance = this.InitialBalance;
            accountModel.Status = this.Status;
            accountModel.IsDeleted = this.IsDeleted;
        }
    }
}
