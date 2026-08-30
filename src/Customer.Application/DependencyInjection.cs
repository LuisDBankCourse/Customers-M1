using Customer.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services )
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();

            return services;
        }
    }
}
