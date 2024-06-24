using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using TechnicalPursuitApi.Application.Common.Behaviors;
using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Application.Services;
using TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands;

namespace TechnicalPursuitApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>), ServiceLifetime.Scoped);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>), ServiceLifetime.Scoped);
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<RulesService>();

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddAQuestionCommand>, AddAQuestionCommandValidator>();
        services.AddTransient<IValidator<AddManyQuestionsCommand>, AddManyQuestionsCommandValidator>();

        return services;
    }
}