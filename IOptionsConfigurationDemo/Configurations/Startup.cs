using IOptionsConfigurationDemo.Settings;

namespace IOptionsConfigurationDemo.Configurations
{
    internal static class Startup
    {
        internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                const string configurationsDirectory = "Configurations"; // Path for pickup location of configuration files
                var env = context.HostingEnvironment; // Get current hosting environment

                // Application Specific Configurations
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/swagger.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/swagger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });
            return host;
        }

        internal static IServiceCollection AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerSettings>(options => configuration.GetSection("SwaggerSettings").Bind(options));
            return services;
        }
    }
}
