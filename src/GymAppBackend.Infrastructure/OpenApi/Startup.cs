using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace GymAppBackend.Infrastructure.OpenApi;

public static class SwaggerStartup
{
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument((document, serviceProvider) =>
        {
            document.PostProcess = doc =>
            {
                doc.Info.Title = "GymApp API";
                doc.Info.Version = "v1";
                doc.Info.Description = "API for GymApp workout tracking application";
            };

            document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Input your Bearer token to access this API",
                In = OpenApiSecurityApiKeyLocation.Header,
                Type = OpenApiSecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
            });

            document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
            document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());
            document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());
        });

        return services;
    }

    public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseOpenApi();
        app.UseSwaggerUi(options =>
        {
            options.DefaultModelsExpandDepth = -1;
            options.DocExpansion = "none";
            options.TagsSorter = "alpha";
            options.PersistAuthorization = true;
        });

        return app;
    }
}
