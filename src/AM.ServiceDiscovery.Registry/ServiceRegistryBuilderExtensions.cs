using Microsoft.AspNetCore.Builder;

namespace AM.ServiceDiscovery.Registry
{
    public static class RegistryBuilderExtensions
    {
        public static IApplicationBuilder UseServiceDiscoveryRegistry(this IApplicationBuilder builder, ServiceRegistryOptions options = null)
        {
            if (options == null)
            {
                options = GetDefaultRegistryOptions();
            }

            builder.UseMiddleware<ServiceRegistryMiddleware>();

            return builder;
        }

        private static ServiceRegistryOptions GetDefaultRegistryOptions()
        {
            return new ServiceRegistryOptions
            {
                RegisterUrl = "servicediscovery/register",
                UnregisterUrl = "servicediscovery/unregister"
            };
        }
    }
}
