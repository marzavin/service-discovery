using AM.ServiceDiscovery.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM.ServiceDiscovery.Registry
{
    internal class InMemoryServiceRegistryStore : IServiceRegistryStore
    {
        private List<ServiceRegistrationInfo> _registrations = new List<ServiceRegistrationInfo>();

        public Task<List<ServiceRegistrationInfo>> GetRegisteredServicesAsync()
        {
            return Task.FromResult(new List<ServiceRegistrationInfo>(_registrations));
        }

        public Task<ServiceRegistrationInfo> GetServiceByIdAsync(string id)
        {
            return Task.FromResult(_registrations.FirstOrDefault(x => x.Id == id));
        }

        public Task RegisterServiceAsync(ServiceRegistrationInfo registration)
        {
            if (_registrations.All(x => x.Id != registration.Id))
            {
                _registrations.Add(registration);
            }

            return Task.CompletedTask;
        }

        public Task UnregisterServiceAsync(ServiceRegistrationInfo registration)
        {
            _registrations.RemoveAll(x => x.Id == registration.Id);
            
            return Task.CompletedTask;
        }
    }
}
