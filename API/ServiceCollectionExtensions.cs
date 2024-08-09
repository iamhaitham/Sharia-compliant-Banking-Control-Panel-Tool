using Core.Utilities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace API;

public static class ServiceCollectionExtensions
{
    public static void AddSecurityToSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Authorization header using the Bearer scheme: \"bearer <token>\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
    
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }

    public static void AddCustomizedJwtAuthorization(
        this IServiceCollection serviceCollection, 
        IConfiguration configuration
    )
    {
        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration[AppSettingsJsonKeys.JwtKeyFromAppSettings]!)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}