using Xunit;

namespace HtmlEncodeTests.IntegrationTests
{
    [CollectionDefinition(nameof(TestServerFixtureCollection))]
    public class TestServerFixtureCollection : ICollectionFixture<TestServerFixture> { }
}
