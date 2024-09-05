using WebApp.Core.Accounts.EventModels;
using Marten;
using MediatR;

namespace WebApp.Core.Accounts.Commands
{
    public class CommandHandlers : IRequestHandler<CreateAccountCommand, CreateAccountResponse>,
                                    IRequestHandler<DeleteAccountCommand, DeleteAccountResponse>,
                                    IRequestHandler<MoneyDepositedAccountCommand, MoneyDepositedAccountResponse>
    {
        private readonly IDocumentSession _dbSession;

        public CommandHandlers(IDocumentSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request,
            CancellationToken cancellationToken)
        {
            var newAccountId = Guid.NewGuid();

            _dbSession.Events.StartStream<AccountModel>(newAccountId, new AccountCreatedEventModel
            {
                AccountId = newAccountId,
                Owner = request.NameSurname,
                InitialBalance = request.InitialMoney,
            });

            await _dbSession.SaveChangesAsync();

            return new CreateAccountResponse(newAccountId);
        }

        public async Task<DeleteAccountResponse> Handle(DeleteAccountCommand request,
                            CancellationToken cancellationToken)
        {
            var account = await _dbSession.Events.AggregateStreamAsync<AccountModel>(request.Id);

            if (account == null || account.IsDeleted)
            {
                return new DeleteAccountResponse(false);
            }

            var deleteEvent = new AccountDeletedEventModel { AccountId = account.Id };

            _dbSession.Events.Append(account.Id, deleteEvent);

            await _dbSession.SaveChangesAsync();

            return new DeleteAccountResponse(true);
        }

        public async Task<MoneyDepositedAccountResponse> Handle(MoneyDepositedAccountCommand request,
                                                                CancellationToken cancellationToken)
        {
            var account = await _dbSession.Events.AggregateStreamAsync<AccountModel>(request.AccountId);

            if (account == null || account.IsDeleted)
            {
                return new MoneyDepositedAccountResponse(false);
            }

            _dbSession.Events.Append(request.AccountId, new MoneyDepositedEventModel
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
            });

            await _dbSession.SaveChangesAsync();

            return new MoneyDepositedAccountResponse(true);
        }
    }
}
