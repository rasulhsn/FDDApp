
namespace WebApp.Core.Accounts.EventModels
{
    public class AccountBlockedEventModel : IEventModel<AccountModel>
    {
        public Guid AccountId { get; set; }
        public AccountStatus Status => AccountStatus.Blocked;

        public AccountBlockedEventModel() { }

        public void Apply(AccountModel model)
        {
            model.Status = this.Status;
        }
    }
}
