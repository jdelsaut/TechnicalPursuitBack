using System.Data.Common;

using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Respawn;

using TechnicalPursuitApi.Api;
using TechnicalPursuitApi.Infrastructure;

using Testcontainers.MsSql;

namespace TechnicalPursuitApi.Api.IntegrationTests;

public class TechnicalPursuitApiApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer =
        new MsSqlBuilder()
            .WithImage("docker.hg-repo.implisis.ch/mssql/server:2022-latest")
            .Build();
    private string _connectionString = null!;
    private Respawner _respawner = default!;

    public HttpClient HttpClient { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        _connectionString = _msSqlContainer.GetConnectionString();
        HttpClient = CreateClient();
        await InitializeRespawner();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public new async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<TechnicalPursuitApiDbContext>) == service.ServiceType)!);
            services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType)!);
            services.AddDbContext<TechnicalPursuitApiDbContext>((_, option) => option.UseSqlServer(_connectionString));

            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
        });
    }

    private async Task InitializeRespawner()
    {
        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
        });
    }
}