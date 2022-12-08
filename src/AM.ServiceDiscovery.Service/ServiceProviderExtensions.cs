using Microsoft.AspNetCore.Builder;
using System;

namespace AM.ServiceDiscovery.Service
{
    public static class ServiceProviderExtensions
    {
        public static IApplicationBuilder UseServiceDiscoveryHealthCheck(this IApplicationBuilder builder, Action<ServiceHealthCheckOptions> configureOptions = null)
        {
            var options = ServiceHealthCheckOptions.Default;
            if (configureOptions != null)
            {
                configureOptions.Invoke(options);
            }

            builder.UseMiddleware<ServiceHealthCheckMiddleware>(options);

            return builder;
        }
    }
}
