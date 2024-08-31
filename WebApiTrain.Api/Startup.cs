﻿using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace WebApiTrain.Api
{
    public static class Startup
    {
        /// <summary>
        /// Подключение Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning()
                .AddApiExplorer(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "WebApiTrain.Api",
                    Description = "Version 1.0",
                    TermsOfService = new Uri("https://ya.ru/"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Test contact",
                        Url = new Uri("https://www.google.ru/")
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Version = "v2",
                    Title = "WebApiTrain.Api",
                    Description = "Version 2.0",
                    TermsOfService = new Uri("https://ya.ru/"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Test contact",
                        Url = new Uri("https://www.google.ru/")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Введите валидный токен",
                    Name = "Авторизация",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name="Bearer",
                            In= ParameterLocation.Header
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
