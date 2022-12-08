namespace AM.ServiceDiscovery.Registry.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;
            services.AddControllers();
            services.AddServiceRegistry();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseServiceDiscoveryRegistry();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}