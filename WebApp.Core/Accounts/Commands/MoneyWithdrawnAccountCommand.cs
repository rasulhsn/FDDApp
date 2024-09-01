using MediatR;

namespace WebApp.Core.Accounts.Commands
{
    public record MoneyWithdrawnAccountResponse();
    public record MoneyWithdrawnAccountCommand() : IRequest<MoneyWithdrawnAccountResponse>;
}
