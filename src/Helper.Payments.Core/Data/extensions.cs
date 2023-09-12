using Helper.Payments.Core.Integrations;
using Helper.Payments.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Payments.Core.Data
{
    internal static class Extensions
    {
        private const string OptionsSectionName = "ConnectionStrings";

        public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var sqlOptions = configuration.GetOptions<SqlOptions>(OptionsSectionName);
            services.AddDbContext<PaymentDbContext>(x => x.UseSqlServer(sqlOptions.AZURE_SQL_CONNECTIONSTRING));
            services.AddHostedService<DatabaseInitializer>();

            return services;
        }
    }
}
