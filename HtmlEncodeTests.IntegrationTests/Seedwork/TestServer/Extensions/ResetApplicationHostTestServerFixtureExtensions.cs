using HtmlEncodeTests.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace HtmlEncodeTests.IntegrationTests
{
    public static class ResetApplicationHostTestServerFixtureExtensions
    {
        public static void OnTestInitResetApplicationState(this TestServerFixture given)
        {
            var models = given.Server.Services.GetRequiredService<List<TypedModel>>();
            models.Clear();

            var dicts = given.Server.Services.GetRequiredService<List<Dictionary<string, string>>>();
            dicts.Clear();
        }
    }
}
