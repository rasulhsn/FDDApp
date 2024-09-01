using MediatR;

namespace WebApp.Core.Accounts.Commands
{
    public record DeleteAccountResponse(bool IsDeleted);
    public record DeleteAccountCommand(Guid Id) : IRequest<DeleteAccountResponse>;
}
