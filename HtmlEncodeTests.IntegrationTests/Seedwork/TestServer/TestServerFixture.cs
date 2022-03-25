using HtmlEncodeTests.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;

namespace HtmlEncodeTests.IntegrationTests
{
    public sealed class TestServerFixture : IDisposable
    {
        public TestServer Server { get; }
        private static TestServerFixture FixtureInstance { get; set; }

        public static void OnTestInitResetApplicationState()
        {
            FixtureInstance.OnTestInitResetApplicationState();
        }

        public TestServerFixture()
        {
            IHostBuilder hostBuilder = ConfigureHost();
            var host = hostBuilder.StartAsync().GetAwaiter().GetResult();
            Server = host.GetTestServer();
            FixtureInstance = this;
        }

        public void Dispose() => Server.Dispose();

        private static IHostBuilder ConfigureHost()
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    
                })
                .UseEnvironment("Test")
                .ConfigureServices(services =>
                {

                })
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                    webHost.ConfigureTestServices(services =>
                    {

                    });
                });
        }
    }
}
