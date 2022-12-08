namespace AM.ServiceDiscovery.Service
{
    public class ServiceHealthCheckOptions
    {
        public static ServiceHealthCheckOptions Default
            => new ServiceHealthCheckOptions
            {
                Url = "healthcheck"
            };

        public string Url { get; set; }
    }
}
