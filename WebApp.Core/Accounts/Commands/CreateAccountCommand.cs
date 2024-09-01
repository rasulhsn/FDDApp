using MediatR;

namespace WebApp.Core.Accounts.Commands
{
    public record CreateAccountResponse(Guid Id);
    public record CreateAccountCommand(string NameSurname, decimal InitialMoney) : IRequest<CreateAccountResponse>;
}
