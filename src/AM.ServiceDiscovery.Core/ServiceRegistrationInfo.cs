using System;

namespace AM.ServiceDiscovery.Core
{
    public class ServiceRegistrationInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string BaseUrl { get; set; }

        public string HealthCheckUrlPath { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}
