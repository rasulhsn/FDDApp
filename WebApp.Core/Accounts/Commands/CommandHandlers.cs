using AccountsWebApp.Core.Accounts.EventModels;
using Marten;
using MediatR;
using WebApp.Core.Accounts.EventModels;

namespace AccountsWebApp.Core.Accounts.Commands
{
    public class CommandHandlers : IRequestHandler<CreateAccountCommand, CreateAccountResponse>,
                                    IRequestHandler<DeleteAccountCommand, DeleteAccountResponse>
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

            _dbSession.Events.StartStream<Account>(newAccountId, new AccountCreated
            {
                AccountId = newAccountId,
                Owner = request.NameSurname,
                InitialBalance = request.InitialMoney,
            });

            //_dbSession.Events.Append(accountId, new MoneyDeposited
            //{
            //    AccountId = accountId,
            //    Amount = 500m
            //});
            //_dbSession.Events.Append(accountId, new MoneyWithdrawn
            //{
            //    AccountId = accountId,
            //    Amount = 200m
            //});

            await _dbSession.SaveChangesAsync();

            return new CreateAccountResponse(newAccountId);
        }

        public async Task<DeleteAccountResponse> Handle(DeleteAccountCommand request,
                            CancellationToken cancellationToken)
        {
            var account = await _dbSession.Events.AggregateStreamAsync<Account>(request.Id);

            if (account == null || account.IsDeleted)
            {
                return new DeleteAccountResponse(false);
            }

            account.Delete();

            _dbSession.Events.Append(account.Id, new AccountDeleted { AccountId = account.Id });

            await _dbSession.SaveChangesAsync();

            return new DeleteAccountResponse(true);
        }
    }
}
