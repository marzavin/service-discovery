using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AM.ServiceDiscovery.Service
{
    internal class ServiceHealthCheckMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ServiceHealthCheckOptions _options;

        private readonly ILogger _logger;

        public ServiceHealthCheckMiddleware(RequestDelegate next, ServiceHealthCheckOptions options, ILogger<ServiceHealthCheckMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Invoke(HttpContext context)
        {
            return _next.Invoke(context);
        }
    }
}
