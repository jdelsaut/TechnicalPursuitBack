using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using TechnicalPursuitApi.Application.Common.Interfaces.Persistence;
using TechnicalPursuitApi.Application.Common.Interfaces.Services;
using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Infrastructure.Persistence.Interceptors;
using TechnicalPursuitApi.Infrastructure.Persistence.Repositories;
using TechnicalPursuitApi.Infrastructure.Services;
using TechnicalPursuitApi.Infrastructure.Settings;

namespace TechnicalPursuitApi.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(
        this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddAuth()
            .AddPersistence(builder.Configuration)
            .RegisterRepositories();

        builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        builder.Services
            .AddHealthChecks()
            .AddSqlServer(
                 builder.Configuration.GetConnectionString("DefaultConnection")!);

        builder
            .ConfigureOpenTelemetry()
            .ConfigureSwagger();

        return builder;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContext<TechnicalPursuitApiDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<PublishDomainEventInterceptor>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TechnicalPursuitApiDbContext>());

        return services;
    }

    public static IHostApplicationBuilder ConfigureSwagger(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"{builder.Environment.EnvironmentName.ToUpper()} - TechnicalPursuitApi - .NET Core (.NET 8) Web API",
            });

            options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the token: `Bearer token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme,
                        },
                    },
                    new string[] { }
                },
            });
        });

        builder.Services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddRuntimeInstrumentation()
                       .AddBuiltInMeters();
            })
            .WithTracing(tracing =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    tracing.SetSampler(new AlwaysOnSampler());
                }

                tracing.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation();
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
            builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
        }

        // Configure alternative exporters
        builder.Services.AddOpenTelemetry()
                        .WithMetrics(metrics =>
                        {
                            // Uncomment the following line to enable the Prometheus endpoint
                            // metrics.AddPrometheusExporter();
                        });

        return builder;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IQuestionRepository), typeof(QuestionRepository));

        // services.AddTransient(typeof(IJoueurRepository), typeof(JoueurRepository));
        return services;
    }

    private static MeterProviderBuilder AddBuiltInMeters(this MeterProviderBuilder meterProviderBuilder) =>
        meterProviderBuilder.AddMeter(
            "Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel",
            "System.Net.Http");

    private static IServiceCollection AddAuth(
        this IServiceCollection services)
    {
        services.AddAuthentication();

        return services;
    }
}