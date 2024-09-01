using MediatR;

namespace AccountsWebApp.Core.Accounts.Commands
{
    public record MoneyWithdrawnAccountResponse();
    public record MoneyWithdrawnAccountCommand() : IRequest<MoneyWithdrawnAccountResponse>;
}
