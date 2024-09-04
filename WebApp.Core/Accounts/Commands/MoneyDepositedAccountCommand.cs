using MediatR;

namespace WebApp.Core.Accounts.Commands
{
    public record MoneyDepositedAccountResponse(bool isSuccess);
    public record MoneyDepositedAccountCommand(Guid AccountId, decimal Amount) : IRequest<MoneyDepositedAccountResponse>;
}
