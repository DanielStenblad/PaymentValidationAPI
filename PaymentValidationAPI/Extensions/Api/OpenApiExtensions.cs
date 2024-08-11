using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

public static class OpenApiExtensions
{

    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Payment Validation API",
                Version = "v1",
                Description = "The PaymentValidationAPI is a service designed to validate and verify various payment methods to ensure secure and reliable transactions.",                
            });
            c.EnableAnnotations();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);
        });

        services.AddSwaggerGen(options => options.ExampleFilters());
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        return services;
    }

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        return app;
    }
}