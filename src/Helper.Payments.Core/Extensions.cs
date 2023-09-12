using Helper.Payments.Core.Abstractions;
using Helper.Payments.Core.Data;
using Helper.Payments.Core.Integrations;
using Helper.Payments.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Payments.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMessageBrokerClient, RabbitMQIntegration>();
            services.AddSql(configuration);

            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            // rodzaje cyklu życia 
            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
