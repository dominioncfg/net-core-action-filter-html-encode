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

        [Fact]
        [ResetApplicationState]
        public async Task DoesNotEncodeWhenStringIsClean()
        {
            var key = "stringField";
            var originalString = "Jelou wold";
            var request = new EncodeDictBuilder()
                .WithString(key, originalString)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedDicts();
            modelsInDb.Should().NotBeNull().And.HaveCount(1);
           
            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull().And.HaveCount(1);
            firstModel.ContainsKey(key).Should().Be(true);
            firstModel[key].Should().Be(originalString);          
        }

        [Fact]
        [ResetApplicationState]
        public async Task DoesEncodeWhenStringIsContainsTags()
        {
            var key = "stringField";
            string originalString = "<string>Hola Mundo</string>";
            string encodedString = "&lt;string&gt;Hola Mundo&lt;/string&gt;";
            var request = new EncodeDictBuilder()
                .WithString(key, originalString)
                .Build();
          
            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedDicts();
            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull().And.HaveCount(1);
            firstModel.ContainsKey(key).Should().Be(true);
            firstModel[key].Should().Be(encodedString);
        }

        [Fact]
        [ResetApplicationState]
        public async Task DoesNotEncodeWhenFieldIsMarkAsIgnoreOnControllerAction()
        {
            var key = "stringField";
            var ignoreKey = "ignoreStringField";
            string originalString = "<string>Hola Mundo</string>";
            string anotherString = "<string>Hola Mundo2</string>";
            string encodedString = "&lt;string&gt;Hola Mundo&lt;/string&gt;";
            var request = new EncodeDictBuilder()
                .WithString(key, originalString)
                .WithString(ignoreKey, anotherString)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedDicts();
            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull().And.HaveCount(2);
            
            firstModel.ContainsKey(key).Should().Be(true);
            firstModel[key].Should().Be(encodedString);

            firstModel.ContainsKey(ignoreKey).Should().Be(true);
            firstModel[ignoreKey].Should().Be(anotherString);
        }

        public static class ApiHelper
        {
            public static class Post
            {
                public static string PostModelUrl() => "api/v1/encode/dict";
            }
        }
    }

}
