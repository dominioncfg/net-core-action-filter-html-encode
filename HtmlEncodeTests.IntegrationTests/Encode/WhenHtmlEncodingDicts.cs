using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HtmlEncodeTests.IntegrationTests
{
    [Collection(nameof(TestServerFixtureCollection))]
    public class WhenHtmlEncodingDicts
    {
        private readonly Guid Id = Guid.NewGuid();
        private readonly TestServerFixture Given;

        public WhenHtmlEncodingDicts(TestServerFixture given)
        {
            Given = given ?? throw new Exception("Null Server");
        }

        /*[Fact]
        [ResetApplicationState]
        public async Task DoesNotEncodeWhenStringIsClean()
        {
            var request = new EncodeDictBuilder()
                .WithIntField("intField", 4)
                .WithString("stringField", "Jelou wold")
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedDicts();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            foreach (var field in request)
            {
                firstModel.ContainsKey(field.Key).Should().Be(true);
                firstModel[field.Key].Should().Be(field.Value);
            }
        }*/

        public static class ApiHelper
        {
            public static class Post
            {
                public static string PostModelUrl() => "api/v1/encode/dict";
            }
        }
    }

}
