
namespace WebApp.Core.Accounts.EventModels
{
    public interface IEventModel<in T> where T : class
    {
        public void Apply(T instance);
    }
}
