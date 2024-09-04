using WebApp.Core.Accounts.EventModels;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Core
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

                opts.Events.AddEventType(typeof(AccountCreatedEventModel));
                opts.Events.AddEventType(typeof(AccountDeletedEventModel));
                opts.Events.AddEventType(typeof(MoneyDepositedEventModel));
                opts.Events.AddEventType(typeof(MoneyWithdrawnEventModel));
            });

            return services;
        }
    }
}
