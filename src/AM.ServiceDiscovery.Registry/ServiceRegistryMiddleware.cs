using AM.ServiceDiscovery.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AM.ServiceDiscovery.Registry
{
    public class ServiceRegistryMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ServiceRegistryOptions _options;

        private readonly IServiceRegistryStore _store;

        private readonly ILogger _logger;

        public ServiceRegistryMiddleware(RequestDelegate next, ServiceRegistryOptions options,
            IServiceRegistryStore store, ILogger<ServiceRegistryMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Invoke(HttpContext context)
        {
            return ValidateRequest(context)
                ? ProcessRequestAsync(context)
                : _next.Invoke(context);
        }

        private bool ValidateRequest(HttpContext context)
        {
            var request = context.Request;
            return CheckSupportedUrl(request.Path, _options.RegisterUrl) || CheckSupportedUrl(request.Path, _options.UnregisterUrl);
        }

        private async Task ProcessRequestAsync(HttpContext context)
        {
            var request = context.Request;
            var status = 200;
            string message;

            var service = GetServiceRegistration(request);
            if (ValidateServiceRegistration(service))
            {
                try
                {
                    if (CheckSupportedUrl(request.Path, _options.RegisterUrl))
                    {
                        await _store.RegisterServiceAsync(service);
                        message = $"Service with Id='{service.Id}' has been successfully registered.";
                    }
                    else
                    {
                        await _store.UnregisterServiceAsync(service);
                        message = $"Service with Id='{service.Id}' has been successfully unregistered.";
                    }

                    _logger.LogInformation(message);
                }
                catch (Exception ex)
                {
                    status = 500;
                    message = $"An error occured during processing the request '{request.Path}'.";

                    _logger.LogError(ex, message);
                }
            }
            else
            {
                status = 400;
                message = "Invalid or incomplete information about the service.";

                _logger.LogError(message);
            }

            SetResponse(context.Response, status, message);
            return;
        }

        private bool CheckSupportedUrl(string path, string supportedUrl)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return path.Trim('/', '\\').Trim().ToLowerInvariant() == supportedUrl.Trim('/', '\\').Trim().ToLowerInvariant();
        }

        private bool ValidateServiceRegistration(ServiceRegistrationInfo service)
        {
            return !string.IsNullOrWhiteSpace(service.Id)
                && !string.IsNullOrWhiteSpace(service.Name)
                && !string.IsNullOrWhiteSpace(service.BaseUrl);
        }

        private void SetResponse(HttpResponse response, int code, string reasonPhase)
        {
            response.StatusCode = code;
            response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = reasonPhase;
        }

        private ServiceRegistrationInfo GetServiceRegistration(HttpRequest request)
        {
            return new ServiceRegistrationInfo { TimeStamp = DateTime.UtcNow };
        }
    }
}
