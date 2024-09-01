namespace AccountsWebApp.Core.Accounts.EventModels
{
    public class AccountCreated
    {
        public Guid AccountId { get; set; }
        public string Owner { get; set; }
        public decimal InitialBalance { get; set; }

        public bool IsDeleted = false;
    }
}
