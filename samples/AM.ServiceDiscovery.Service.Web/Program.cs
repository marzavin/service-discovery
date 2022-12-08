using AM.ServiceDiscovery.Core;

namespace AM.ServiceDiscovery.Service.Web
{
    public class Program
    {
        private static readonly ServiceRegistryClient _serviceRegistry = new();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(OnStart);
            app.Lifetime.ApplicationStopped.Register(OnStop);

            // Configure the HTTP request pipeline.

            app.UseServiceDiscoveryHealthCheck();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void OnStart()
        {
            var service = GetServiceRegistrationInfo();
            _serviceRegistry.RegisterService(service);
        }

        private static void OnStop()
        {
            var service = GetServiceRegistrationInfo();
            _serviceRegistry.UnregisterService(service);
        }

        private static ServiceRegistrationInfo GetServiceRegistrationInfo()
        {
            return new ServiceRegistrationInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                BaseUrl = Guid.NewGuid().ToString(),
                TimeStamp = DateTime.UtcNow
            };
        }
    }
}