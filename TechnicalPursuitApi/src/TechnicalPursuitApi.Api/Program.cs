using Asp.Versioning.ApiExplorer;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Serilog;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;
using Serilog.Sinks.Elasticsearch;

using TechnicalPursuitApi.Api;
using TechnicalPursuitApi.Application;
using TechnicalPursuitApi.Infrastructure;
using TechnicalPursuitApi.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .RegisterValidators();
}

builder.AddInfrastructure();

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    var serilogOptions = new SerilogOptions();
    var sectionName = "Serilog";
    var elasticSearchLogDataStream = builder.Configuration.GetSection("ElasticSearch:LogDataStream").Value;
    var elasticSearchUri = builder.Configuration.GetSection("ElasticSearch:Uri").Value;

    builder.Configuration.GetSection(sectionName).Bind(serilogOptions);

    // https://github.com/serilog/serilog-settings-configuration
    loggerConfiguration.ReadFrom.Configuration(context.Configuration, new ConfigurationReaderOptions() { SectionName = sectionName });

    // https://rehansaeed.com/logging-with-serilog-exceptions/
    loggerConfiguration
        .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails();

    if (serilogOptions.UseConsole)
    {
        // https://github.com/serilog/serilog-sinks-async
        loggerConfiguration.WriteTo.Async(writeTo =>
            writeTo.Console(outputTemplate: serilogOptions.LogTemplate));
    }

    if (!string.IsNullOrEmpty(elasticSearchUri))
    {
        loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchUri))
        {
            IndexFormat = elasticSearchLogDataStream,
            AutoRegisterTemplate = true,
            DetectElasticsearchVersion = false,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            TypeName = null,
            BatchAction = ElasticOpType.Create,
        });
    }
});

var app = builder.Build();
{
    var apiVersionDescriptionProvider = app.Services
        .GetRequiredService<IApiVersionDescriptionProvider>();

    // Migrate latest database changes during startup
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<TechnicalPursuitApiDbContext>();

        // Here is the migration executed
        await dbContext.Database.EnsureCreatedAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/error-development");
    }
    else
    {
        app.UseExceptionHandler("/error");
    }

    app.UseHttpsRedirection();

    app.MapHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        ResultStatusCodes =
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
        },
    });

    // Add Swagger
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

    // Add Serilog requests logging
    app.UseSerilogRequestLogging(options =>
    {
        // Customize the message template
        options.MessageTemplate = "Handled {RequestPath}";

        // Attach additional properties to the request completion event
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        };
    });

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}