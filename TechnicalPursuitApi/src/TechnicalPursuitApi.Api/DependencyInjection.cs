using Microsoft.AspNetCore.Mvc.Infrastructure;

using TechnicalPursuitApi.Api.Common.Errors;
using TechnicalPursuitApi.Api.Common.Mapping;

namespace TechnicalPursuitApi.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, TechnicalPursuitApiProblemDetailsFactory>();

        services.AddMappings();

        return services;
    }
}