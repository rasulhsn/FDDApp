using MediatR;

namespace WebApp.Core.Accounts.Commands
{
    public record MoneyWithDrawnAccountResponse();
    public record MoneyWithDrawnAccountCommand() : IRequest<MoneyWithDrawnAccountResponse>;
}
