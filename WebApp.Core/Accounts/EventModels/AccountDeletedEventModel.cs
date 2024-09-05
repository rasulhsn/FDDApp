

namespace WebApp.Core.Accounts.EventModels
{
    public class AccountDeletedEventModel : IEventModel<AccountModel>
    {
        public Guid AccountId { get; set; }

        public void Apply(AccountModel accountModel)
        {
            accountModel.IsDeleted = false;
        }
    }
}
