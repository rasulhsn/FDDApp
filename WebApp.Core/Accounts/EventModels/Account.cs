using WebApp.Core.Accounts.EventModels;

namespace AccountsWebApp.Core.Accounts.EventModels
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Owner { get; private set; }
        public decimal Balance { get; private set; }
        public bool IsDeleted { get; private set; }

        public Account() { }

        public Account(Guid id, string owner, decimal initialBalance)
        {
            Apply(new AccountCreated
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

            Apply(new AccountDeleted() { AccountId = Id });
        }

        public void Deposit(decimal amount)
        {
            if (this.IsDeleted)
            {
                throw new InvalidOperationException("Cannot operate on a deleted account.");
            }

            Apply(new MoneyDeposited
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

            Apply(new MoneyWithdrawn
            {
                AccountId = Id,
                Amount = amount
            });
        }

        public void Apply(object @event)
        {
            switch (@event)
            {
                case AccountCreated e:
                    Id = e.AccountId;
                    Owner = e.Owner;
                    Balance = e.InitialBalance;
                    IsDeleted = e.IsDeleted;
                    break;
                case AccountDeleted e:
                    IsDeleted = true;
                    break;
                case MoneyDeposited e:
                    Balance += e.Amount;
                    break;
                case MoneyWithdrawn e:
                    Balance -= e.Amount;
                    break;  
            }
        }
    }

}
