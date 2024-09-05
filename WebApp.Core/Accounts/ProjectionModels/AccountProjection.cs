using Marten.Events.Aggregation;
using WebApp.Core.Accounts.EventModels;

namespace WebApp.Core.Accounts.ProjectionModels
{
    public class AccountProjection : SingleStreamProjection<AccountEventModel>
    {
        public static AccountEventModel Create(AccountCreatedEventModel accountCreated)
        {
            return new AccountEventModel(accountCreated.AccountId,
                                          accountCreated.Owner,
                                          accountCreated.InitialBalance);
        }

        public void Apply(AccountDeletedEventModel accountDeleted, AccountEventModel account)
        {
            account.Delete();
        }

        public void Apply(MoneyDepositedEventModel moneyDeposited, AccountEventModel account)
        {
            account.Deposit(moneyDeposited.Amount);
        }

        public void Apply(MoneyWithdrawnEventModel moneyWithdrawn, AccountEventModel account)
        {
            account.Withdraw(moneyWithdrawn.Amount);
        }
    }
}
