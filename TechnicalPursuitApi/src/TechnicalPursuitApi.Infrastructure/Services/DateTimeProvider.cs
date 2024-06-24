using TechnicalPursuitApi.Application.Common.Interfaces.Services;

namespace TechnicalPursuitApi.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}