using HtmlEncodeTests.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace HtmlEncodeTests.IntegrationTests
{
    public static class EncodedContentExtensions
    {
        public static List<TypedModel> GetEncodedModels(this TestServerFixture given)
        {
            var contentContainer = given.Server.Services.GetRequiredService<IEncodedContentContainer>();
            return contentContainer.GetAllModels().ToList();
        }

        public static List<Dictionary<string,string>> GetEncodedDicts(this TestServerFixture given)
        {
            var contentContainer = given.Server.Services.GetRequiredService<IEncodedContentContainer>();
            return contentContainer.GetAllDicts().ToList();
        }
    }
}
