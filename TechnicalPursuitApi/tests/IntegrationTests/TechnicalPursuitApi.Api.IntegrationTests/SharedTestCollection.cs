using TechnicalPursuitApi.Api.IntegrationTests;

[CollectionDefinition(nameof(SharedTestCollection))]
public class SharedTestCollection : ICollectionFixture<TechnicalPursuitApiApiFactory>
{
}