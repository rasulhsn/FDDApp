using Marten;
using MediatR;
using WebApp.Core.Accounts.EventModels;

namespace WebApp.Core.Accounts.Queries
{
    public class QueryHandlers : IRequestHandler<GetAccountQuery, GetAccountResponse>,
                                IRequestHandler<GetAllAccountQuery, GetAllAccountResponse>
    {
        private readonly IDocumentStore _documentStore;

        public QueryHandlers(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<GetAccountResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            await using var session = _documentStore.QuerySession();

            var account = await session.LoadAsync<AccountModel>(request.AccountId);

            return new GetAccountResponse(account.Owner, account.Balance);
        }

        public async Task<GetAllAccountResponse> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            await using var session = _documentStore.QuerySession();

            var accounts = await session.Query<AccountModel>().ToListAsync();

            return new GetAllAccountResponse(accounts);
        }
    }
}
