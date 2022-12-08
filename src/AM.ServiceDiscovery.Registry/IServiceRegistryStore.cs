using AM.ServiceDiscovery.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AM.ServiceDiscovery.Registry
{
    public interface IServiceRegistryStore
    {
        Task<List<ServiceRegistrationInfo>> GetRegisteredServicesAsync();

        Task<ServiceRegistrationInfo> GetServiceByIdAsync(string id);

        Task RegisterServiceAsync(ServiceRegistrationInfo registration);

        Task UnregisterServiceAsync(ServiceRegistrationInfo registration);
    }
}
