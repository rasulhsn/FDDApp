using MediatR;

namespace AccountsWebApp.Core.Accounts.Queries
{
    public record GetAccountResponse(string NameSurname, decimal CurrentBalance);
    public record GetAccountQuery(Guid AccountId) : IRequest<GetAccountResponse>;
}
