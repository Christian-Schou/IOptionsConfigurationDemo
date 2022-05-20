using IOptionsConfigurationDemo.Settings;
using NJsonSchema.Generation.TypeMappers;

namespace IOptionsConfigurationDemo.OpenAPI
{
    public static class Startup
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            SwaggerSettings? settings = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

            if (settings.Enable)
            {
                services.AddEndpointsApiExplorer();
                services.AddOpenApiDocument((document, serviceProvider) =>
                {
                    document.PostProcess = doc =>
                    {
                        doc.Info.Title = settings.Title;
                        doc.Info.Version = settings.Version;
                        doc.Info.Description = settings.Description;
                        doc.Info.Contact = new()
                        {
                            Name = settings.ContactName,
                            Email = settings.ContactEmail,
                            Url = settings.ContactUrl
                        };
                        doc.Info.License = new()
                        {
                            Name = settings.LicenseName,
                            Url = settings.LicenseUrl
                        };
                    };
                });


                
            }

            return services;
        }

        internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            SwaggerSettings? settings = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

            if (settings.Enable)
            {
                app.UseOpenApi();
                app.UseSwaggerUi3(options =>
                {
                    options.DefaultModelExpandDepth = -1; // Don't expand the schemas and endpoints
                    options.DocExpansion = "none";
                    options.TagsSorter = "alpha";
                });
            }

            return app;
        }
    }
}
