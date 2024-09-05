
namespace WebApp.Core.Accounts.EventModels
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public bool IsDeleted { get; set; }

        public AccountModel() { }

        public AccountModel(Guid id, string owner, decimal initialBalance)
        {
            Id = id;
            Owner = owner;
            Balance = initialBalance;
            IsDeleted = false;
        }

        public void Apply(object @event)
        {
            IEventModel<AccountModel> eventModel = @event as IEventModel<AccountModel>;
            eventModel.Apply(this);
        }
    }
}
