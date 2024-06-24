FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

EXPOSE 80

COPY ".editorconfig" ".editorconfig"
COPY "Directory.Build.props" "Directory.Build.props"
COPY "Directory.Packages.props" "Directory.Packages.props"

COPY "TechnicalPursuitApi/TechnicalPursuitApi.sln" "TechnicalPursuitApi.sln"

COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Api/TechnicalPursuitApi.Api.csproj" "src/TechnicalPursuitApi.Api/TechnicalPursuitApi.Api.csproj"
COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Application/TechnicalPursuitApi.Application.csproj" "src/TechnicalPursuitApi.Application/TechnicalPursuitApi.Application.csproj"
COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Contracts/TechnicalPursuitApi.Contracts.csproj" "src/TechnicalPursuitApi.Contracts/TechnicalPursuitApi.Contracts.csproj"
COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Domain/TechnicalPursuitApi.Domain.csproj" "src/TechnicalPursuitApi.Domain/TechnicalPursuitApi.Domain.csproj"
COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Infrastructure/TechnicalPursuitApi.Infrastructure.csproj" "src/TechnicalPursuitApi.Infrastructure/TechnicalPursuitApi.Infrastructure.csproj"
#COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Client/TechnicalPursuitApi.Client.csproj" "src/TechnicalPursuitApi.Client/TechnicalPursuitApi.Client.csproj"
COPY "TechnicalPursuitApi/src/TechnicalPursuitApi.Sdk/TechnicalPursuitApi.Sdk.csproj" "src/TechnicalPursuitApi.Sdk/TechnicalPursuitApi.Sdk.csproj"

COPY "TechnicalPursuitApi/tests/UnitTests/TechnicalPursuitApi.Api.UnitTests/TechnicalPursuitApi.Api.UnitTests.csproj" "tests/UnitTests/TechnicalPursuitApi.Api.UnitTests/TechnicalPursuitApi.Api.UnitTests.csproj"
COPY "TechnicalPursuitApi/tests/UnitTests/TechnicalPursuitApi.Application.UnitTests/TechnicalPursuitApi.Application.UnitTests.csproj" "tests/UnitTests/TechnicalPursuitApi.Application.UnitTests/TechnicalPursuitApi.Application.UnitTests.csproj"
COPY "TechnicalPursuitApi/tests/UnitTests/TechnicalPursuitApi.Domain.UnitTests/TechnicalPursuitApi.Domain.UnitTests.csproj" "tests/UnitTests/TechnicalPursuitApi.Domain.UnitTests/TechnicalPursuitApi.Domain.UnitTests.csproj"
COPY "TechnicalPursuitApi/tests/UnitTests/TechnicalPursuitApi.Infrastructure.UnitTests/TechnicalPursuitApi.Infrastructure.UnitTests.csproj" "tests/UnitTests/TechnicalPursuitApi.Infrastructure.UnitTests/TechnicalPursuitApi.Infrastructure.UnitTests.csproj"

COPY "TechnicalPursuitApi/tests/IntegrationTests/TechnicalPursuitApi.Api.IntegrationTests/TechnicalPursuitApi.Api.IntegrationTests.csproj" "tests/IntegrationTests/TechnicalPursuitApi.Api.IntegrationTests/TechnicalPursuitApi.Api.IntegrationTests.csproj"
COPY "TechnicalPursuitApi/tests/AcceptanceTests/TechnicalPursuitApi.Api.AcceptanceTests/TechnicalPursuitApi.Api.AcceptanceTests.csproj" "tests/AcceptanceTests/TechnicalPursuitApi.Api.AcceptanceTests/TechnicalPursuitApi.Api.AcceptanceTests.csproj"

RUN dotnet restore "TechnicalPursuitApi.sln"

COPY ./TechnicalPursuitApi/src/ src
#RUN ls -la --recursive

WORKDIR /app/src/TechnicalPursuitApi.Api
RUN dotnet publish --no-restore -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env  /app/out .
ENTRYPOINT ["dotnet", "TechnicalPursuitApi.Api.dll"]