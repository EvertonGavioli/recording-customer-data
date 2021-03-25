using Microsoft.Extensions.DependencyInjection;
using RCD.Core.Notifications;
using RCD.Domain.Repository;
using RCD.Domain.Services;
using RCD.Infrastructure.Data;
using RCD.Infrastructure.Repository;

namespace RCD.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
