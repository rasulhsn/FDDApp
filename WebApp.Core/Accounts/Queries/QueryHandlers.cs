using AccountsWebApp.Core.Accounts.EventModels;
using AccountsWebApp.Core.Accounts.Queries;
using Marten;
using MediatR;

namespace WebApp.Core.Accounts.Queries
{
    public class QueryHandlers : IRequestHandler<GetAccountQuery, GetAccountResponse>
    {
        private readonly IDocumentSession _dbSession;

        public QueryHandlers(IDocumentSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<GetAccountResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var loadedAccount = await _dbSession.Events.AggregateStreamAsync<Account>(request.AccountId);

            if (loadedAccount == null)
            {
                return null;
            }

            return new GetAccountResponse(loadedAccount.Owner, loadedAccount.Balance);
        }
    }
}
