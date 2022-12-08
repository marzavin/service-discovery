namespace AM.ServiceDiscovery.Service.Web
{
    public class Program
    {
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
        { }

        private static void OnStop()
        { }
    }
}