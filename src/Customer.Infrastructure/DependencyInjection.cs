using Customer.Domain.Repositories;
using Customer.Infrastructure.Persistence;
using Customer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(opt =>
             opt.UseSqlServer(configuration.GetConnectionString("CustomerConnection"))
            );

            services.AddScoped<ICustomerRepository,CustomerRepository>();

            return services;

        }
    }
}
