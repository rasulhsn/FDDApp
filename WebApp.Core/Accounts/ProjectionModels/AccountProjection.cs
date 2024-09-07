using Marten.Events.Aggregation;
using WebApp.Core.Accounts.EventModels;

namespace WebApp.Core.Accounts.ProjectionModels
{
    public class AccountProjection : SingleStreamProjection<AccountModel>
    {
        public AccountProjection()
        {
            //DeleteEvent<AccountDeletedEventModel>();
        }

        public static AccountModel Create(AccountCreatedEventModel accountCreated)
        {
            var accountModel = new AccountModel();
            accountCreated.Apply(accountModel);
            return accountModel;
        }

        public void Apply(AccountDeletedEventModel accountDeleted, AccountModel account)
        {
            accountDeleted.Apply(account);
        }

        public void Apply(MoneyDepositedEventModel moneyDeposited, AccountModel account)
        {
            moneyDeposited.Apply(account);
        }

        public void Apply(MoneyWithdrawnEventModel moneyWithdrawn, AccountModel account)
        {
            moneyWithdrawn.Apply(account);
        }

        public void Apply(AccountBlockedEventModel blockedEventModel, AccountModel account)
        {
            blockedEventModel.Apply(account);
        }
    }
}
