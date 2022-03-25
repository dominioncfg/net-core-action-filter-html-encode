using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HtmlEncodeTests.IntegrationTests
{
    public static class RequestExtensions
    {
        public static async Task PostAndExpectOkAsync(this TestServerFixture given, string url, object request)
        {
            var client = given.Server.CreateClient();
            var json = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync(url, httpContent);
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
