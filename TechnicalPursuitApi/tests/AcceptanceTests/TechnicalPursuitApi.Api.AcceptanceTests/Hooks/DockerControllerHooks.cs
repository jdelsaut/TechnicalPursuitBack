using System.Net;

using BoDi;

using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;

using Microsoft.Extensions.Configuration;

using TechTalk.SpecFlow;

namespace TechnicalPursuitApi.Api.AcceptanceTests.Hooks;

[Binding]
public class DockerControllerHooks
{
    private static ICompositeService compositeService = default!;
    private readonly IObjectContainer _objectContainer;

    public DockerControllerHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var config = LoadConfiguration();

        var dockerComposeFileName = config["DockerComposeFileName"]!;
        var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

        var confirmationUrl = config["TechnicalPursuitApi.Api:BaseAddress"];
        compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(dockerComposePath)
            .RemoveOrphans()
            .WaitForHttp(
                "TechnicalPursuitApi-api",
                $"{confirmationUrl}/_health",
                continuation: (response, _) =>
                {
                    return response.Code != HttpStatusCode.OK ? 2000 : 0;
                })
            .Build().Start();
    }

    [AfterTestRun]
    public static void DockerComposeDown()
    {
        compositeService.Stop();
        compositeService.Dispose();
    }

    [BeforeScenario]
    public void AddHttpClient()
    {
        var config = LoadConfiguration();
        var httpClient = new HttpClient { BaseAddress = new Uri(config["TechnicalPursuitApi.Api:BaseAddress"]!) };
        _objectContainer.RegisterInstanceAs(httpClient);
    }

    private static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }

    private static string GetDockerComposeLocation(string dockerComposeFileName)
    {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)))
        {
            directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
        }

        return Path.Combine(directory, dockerComposeFileName);
    }
}