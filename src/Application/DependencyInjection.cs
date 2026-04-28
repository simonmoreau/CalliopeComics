using Microsoft.Extensions.DependencyInjection;

namespace Application.Interfaces
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}