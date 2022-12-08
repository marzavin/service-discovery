using Microsoft.Extensions.DependencyInjection;

namespace AM.ServiceDiscovery.Registry
{
    public static class ServiceRegistryServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services)
        {
            return AddServiceRegistry<InMemoryServiceRegistryStore>(services);
        }

        public static IServiceCollection AddServiceRegistry<TStore>(this IServiceCollection services)
            where TStore : class, IServiceRegistryStore
        {
            services.AddSingleton<IServiceRegistryStore, TStore>();
            return services;
        }
    }
}
