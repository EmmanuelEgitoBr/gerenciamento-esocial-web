using Gerenciamento.Informacoes.ESocial.Api.Services;
using Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Gerenciamento.Informacoes.ESocial.Api.Extensions
{
    public static class ApiBuilderExtensions
    {
        public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "API de Gerenciamento de Informações",
                    Version = "v1",
                    Description = "Api destinada para o gerenciamento de informações que estão integradas ao E-Social",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Emmanuel Egito",
                        Url = new Uri("https://github.com/EmmanuelEgitoBr/gerenciamento-esocial-web")
                    }
                });

                // Adiciona o arquivo XML gerado para incluir os comentários
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer JWT",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
                options.SupportNonNullableReferenceTypes();
                options.MapType<IFormFile>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });
            });
        }

        public static void AddAuthConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddCorsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendLocalhost", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000") // obrigatoriamente a origem do front
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // importante para cookies
                });
            });
        }
    }
}
