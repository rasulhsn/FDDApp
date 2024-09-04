using Marten;
using Marten.Events;
using Marten.Events.Projections;
using WebApp.Core.Accounts.EventModels;

namespace WebApp.Core.Accounts.ProjectionModels
{
    public class AccountProjectionModel : IProjection
    {
        public void Apply(IDocumentOperations operations, IReadOnlyList<StreamAction> streams)
        {
            foreach (var stream in streams)
            {
                foreach (var action in stream.Events)
                {
                    switch (action)
                    {
                        case IEvent streamAction:
                            var @event = streamAction.Data;
                            HandleEvent(operations, @event);
                            break;
                    }
                }
            }
        }

        public Task ApplyAsync(IDocumentOperations operations, IReadOnlyList<StreamAction> streams, CancellationToken cancellation)
        {
            this.Apply(operations, streams);
            return Task.CompletedTask;
        }

        private void HandleEvent(IDocumentOperations operations, object @event)
        {
            switch (@event)
            {
                case AccountCreatedEventModel e:
                    var createdAccount = new AccountEventModel(e.AccountId, e.Owner, e.InitialBalance);
                    operations.Store(createdAccount);
                    break;

                case MoneyDepositedEventModel e:
                    var accountDeposite = operations.Load<AccountEventModel>(e.AccountId);
                    if (accountDeposite != null && !accountDeposite.IsDeleted)
                    {
                        accountDeposite.Deposit(e.Amount);
                        operations.Store(accountDeposite);
                    }
                    break;

                case MoneyWithdrawnEventModel e:
                    var accountWithDraw = operations.Load<AccountEventModel>(e.AccountId);
                    if (accountWithDraw != null && !accountWithDraw.IsDeleted)
                    {
                        accountWithDraw.Withdraw(e.Amount);
                        operations.Store(accountWithDraw);
                    }
                    break;

                case AccountDeletedEventModel e:
                    var deletedAccount = operations.Load<AccountEventModel>(e.AccountId);
                    if (deletedAccount != null)
                    {
                        deletedAccount.Delete();
                        operations.Store(deletedAccount);
                    }
                    break;

                default:
                    throw new NotSupportedException($"Event type '{@event.GetType().Name}' is not supported.");
            }
        }
    }
}
