using IOptionsConfigurationDemo.Configurations;
using IOptionsConfigurationDemo.OpenAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddConfigurations();
builder.Services.AddConfigurationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApiDocumentation(builder.Configuration); // Add OpenAPI

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseOpenApiDocumentation(builder.Configuration);

app.MapControllers();

app.Run();
