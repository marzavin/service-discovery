using Microsoft.AspNetCore.Builder;
using System;

namespace AM.ServiceDiscovery.Registry
{
    public static class RegistryBuilderExtensions
    {
        public static IApplicationBuilder UseServiceDiscoveryRegistry(this IApplicationBuilder builder, Action<ServiceRegistryOptions> configureOptions = null)
        {
            var options = ServiceRegistryOptions.Default;
            if (configureOptions != null)
            {
                configureOptions.Invoke(options);
            }

            builder.UseMiddleware<ServiceRegistryMiddleware>(options);

            return builder;
        }
    }
}
