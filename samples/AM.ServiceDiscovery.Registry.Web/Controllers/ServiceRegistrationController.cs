using AM.ServiceDiscovery.Core;
using Microsoft.AspNetCore.Mvc;

namespace AM.ServiceDiscovery.Registry.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceRegistrationController : ControllerBase
    {
        private readonly IServiceRegistryStore _store;

        private readonly ILogger<ServiceRegistrationController> _logger;
       
        public ServiceRegistrationController(IServiceRegistryStore store, ILogger<ServiceRegistrationController> logger)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceRegistrationInfo>> Index()
        {
            return await _store.GetRegisteredServicesAsync();
        }
    }
}