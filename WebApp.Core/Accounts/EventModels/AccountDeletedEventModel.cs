

namespace WebApp.Core.Accounts.EventModels
{
    public class AccountDeletedEventModel : IEventModel<AccountModel>
    {
        public Guid AccountId { get; set; }
        public bool IsDeleted => true;

        public void Apply(AccountModel accountModel)
        {
            accountModel.IsDeleted = this.IsDeleted;
        }
    }
}
