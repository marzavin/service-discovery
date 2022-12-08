namespace AM.ServiceDiscovery.Registry
{
    public class ServiceRegistryOptions
    {
        public static ServiceRegistryOptions Default 
            => new ServiceRegistryOptions
            {
                RegisterUrl = "servicediscovery/register",
                UnregisterUrl = "servicediscovery/unregister",
                HealthCheckPeriod = 60
            };

        public string RegisterUrl { get; set; }

        public string UnregisterUrl { get; set; }

        public long HealthCheckPeriod { get; set; }
    }
}