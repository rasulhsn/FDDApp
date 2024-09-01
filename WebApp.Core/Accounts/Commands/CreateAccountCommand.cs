using MediatR;

namespace AccountsWebApp.Core.Accounts.Commands
{
    public record CreateAccountResponse(Guid Id);
    public record CreateAccountCommand(string NameSurname, decimal InitialMoney) : IRequest<CreateAccountResponse>;
}
