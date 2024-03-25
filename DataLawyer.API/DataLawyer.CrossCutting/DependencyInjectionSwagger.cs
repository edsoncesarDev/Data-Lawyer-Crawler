
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DataLawyer.CrossCutting;

public static class DependencyInjectionSwagger
{
    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataLawyer.API", Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Edson César",
                    Email = "edson.dev10@gmail.com",
                    Url = new Uri("https://br.linkedin.com/in/edson-cesar-1a5067a7")
                }
            });

            // configurando token Jwt na interface do Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = @"JWT Authorization header using the Bearer scheme.
                                    Enter with 'Bearer' [space] and then place your token.
                                    Example: 'Bearer 12345abcdef'",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}
