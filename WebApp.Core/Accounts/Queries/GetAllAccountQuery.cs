
using MediatR;
using WebApp.Core.Accounts.EventModels;

namespace WebApp.Core.Accounts.Queries
{
    public record GetAllAccountResponse(IEnumerable<AccountModel> accounts);
    public record GetAllAccountQuery() : IRequest<GetAllAccountResponse>;
}
