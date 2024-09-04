
namespace WebApp.Core.Accounts.EventModels
{
    public class AccountEventModel
    {
        public Guid Id { get; private set; }
        public string Owner { get; private set; }
        public decimal Balance { get; private set; }
        public bool IsDeleted { get; private set; }

        public AccountEventModel() { }

        public AccountEventModel(Guid id, string owner, decimal initialBalance)
        {
            Apply(new AccountCreatedEventModel
            {
                AccountId = id,
                Owner = owner,
                InitialBalance = initialBalance,
                IsDeleted = false
            });
        }

        public void Delete()
        {
            if (this.IsDeleted)
            {
                throw new InvalidOperationException("Cannot operate on a deleted account.");
            }

            Apply(new AccountDeletedEventModel() { AccountId = Id });
        }

        public void Deposit(decimal amount)
        {
            if (this.IsDeleted)
            {
                throw new InvalidOperationException("Cannot operate on a deleted account.");
            }

            Apply(new MoneyDepositedEventModel
            {
                AccountId = Id,
                Amount = amount
            });
        }

        public void Withdraw(decimal amount)
        {
            if (this.IsDeleted)
            {
                throw new InvalidOperationException("Cannot operate on a deleted account.");
            }

            if (Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            Apply(new MoneyWithdrawnEventModel
            {
                AccountId = Id,
                Amount = amount
            });
        }

        public void Apply(object @event)
        {
            switch (@event)
            {
                case AccountCreatedEventModel e:
                    Id = e.AccountId;
                    Owner = e.Owner;
                    Balance = e.InitialBalance;
                    IsDeleted = e.IsDeleted;
                    break;
                case AccountDeletedEventModel e:
                    IsDeleted = true;
                    break;
                case MoneyDepositedEventModel e:
                    Balance += e.Amount;
                    break;
                case MoneyWithdrawnEventModel e:
                    Balance -= e.Amount;
                    break;  
            }
        }
    }

}
