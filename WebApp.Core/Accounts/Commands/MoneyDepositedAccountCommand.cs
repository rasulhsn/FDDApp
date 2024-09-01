using MediatR;

namespace AccountsWebApp.Core.Accounts.Commands
{
    public record MoneyWithDrawnAccountResponse();
    public record MoneyWithDrawnAccountCommand() : IRequest<MoneyWithDrawnAccountResponse>;
}
