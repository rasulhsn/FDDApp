using MediatR;

namespace AccountsWebApp.Core.Accounts.Commands
{
    public record DeleteAccountResponse(bool IsDeleted);
    public record DeleteAccountCommand(Guid Id) : IRequest<DeleteAccountResponse>;
}
