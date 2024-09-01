using AccountsWebApp.Core.Accounts.EventModels;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.Accounts.EventModels;

namespace AccountsWebApp.Core
{
    public static class DIExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
                                                IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DIExtensions).Assembly);
            });

            services.AddMarten(opts =>
            {
                string connectionStr = configuration["DefaultConnectionString"];
                opts.Connection(connectionStr);

                opts.Events.AddEventType(typeof(AccountCreated));
                opts.Events.AddEventType(typeof(AccountDeleted));
                opts.Events.AddEventType(typeof(MoneyDeposited));
                opts.Events.AddEventType(typeof(MoneyWithdrawn));
            });

            return services;
        }
    }
}
