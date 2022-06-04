using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HtmlEncodeTests.IntegrationTests
{
    [Collection(nameof(TestServerFixtureCollection))]
    public class WhenHtmlEncodingModels
    {
        private readonly Guid Id = Guid.NewGuid();
        private readonly TestServerFixture Given;

        public WhenHtmlEncodingModels(TestServerFixture given)
        {
            Given = given ?? throw new Exception("Null Server");
        }

        [Fact]
        [ResetApplicationState]
        public async Task DoesEncodeWhenStringIsContainsTags()
        {
            string originalString = "<string>Hola Mundo</string>";
            string encodedString = "&lt;string&gt;Hola Mundo&lt;/string&gt;";
            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(originalString)
                .WithInnerModel(innerModel => innerModel
                    .WithInt(5)
                    .WithString("simpleString")
                )
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            firstModel.IntField.Should().Be(request.IntField);
            firstModel.StringField.Should().Be(encodedString);

            firstModel.InnerModel.Should().NotBeNull();
            firstModel.InnerModel.InnerIntField.Should().Be(request.InnerModel.InnerIntField);
            firstModel.InnerModel.InnerStringField.Should().Be(request.InnerModel.InnerStringField);
        }

        [Fact]
        [ResetApplicationState]
        public async Task DoesNotEncodeWhenStringIsClean()
        {
            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString("Jelou wold")
                .WithInnerModel(innerModel => innerModel
                    .WithInt(5)
                    .WithString("simpleString")
                )
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            firstModel.IntField.Should().Be(request.IntField);
            firstModel.StringField.Should().Be(request.StringField);

            firstModel.InnerModel.Should().NotBeNull();

            firstModel.InnerModel.InnerIntField.Should().Be(request.InnerModel.InnerIntField);
            firstModel.InnerModel.InnerStringField.Should().Be(request.InnerModel.InnerStringField);
        }


        [Fact]
        [ResetApplicationState]
        public async Task DoesNotFailWhenStringIsNull()
        {
            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(null)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            firstModel.IntField.Should().Be(request.IntField);
            firstModel.StringField.Should().BeNull();
            firstModel.InnerModel.Should().BeNull();
        }

        [Fact]
        [ResetApplicationState]
        public async Task DoesNotFailWhenInnerModelIsNull()
        {
            string originalString = "<string>Hola Mundo</string>";
            string encodedString = "&lt;string&gt;Hola Mundo&lt;/string&gt;";
            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(originalString)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            firstModel.IntField.Should().Be(request.IntField);
            firstModel.StringField.Should().Be(encodedString);

            firstModel.InnerModel.Should().BeNull();
        }


        [Fact]
        [ResetApplicationState]
        public async Task DoesEncodeInnerModels()
        {
            string originalString = "<string>Hola Mundo</string>";
            string anotherStringToEncode = "<string>Hola Mundo2</string>";
            string encodedString = "&lt;string&gt;Hola Mundo&lt;/string&gt;";
            string anotherStringExpectedEncoded = "&lt;string&gt;Hola Mundo2&lt;/string&gt;";
            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(originalString)
                .WithInnerModel(innerModel => innerModel
                    .WithInt(5)
                    .WithString(anotherStringToEncode)
                )
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            firstModel.IntField.Should().Be(request.IntField);
            firstModel.StringField.Should().Be(encodedString);

            firstModel.InnerModel.Should().NotBeNull();
            firstModel.InnerModel.InnerIntField.Should().Be(request.InnerModel.InnerIntField);
            firstModel.InnerModel.InnerStringField.Should().Be(anotherStringExpectedEncoded);
        }

        [Fact]
        [ResetApplicationState]
        public async Task DoesNotEncodePropertyWithIgnoreAttribute()
        {
            string originalString = "<string>Hola Mundo</string>";
            string encodedString = "&lt;string&gt;Hola Mundo&lt;/string&gt;";

            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(originalString)
                .WithNeverEncodeField(originalString)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();
            firstModel.StringField.Should().Be(encodedString);
            firstModel.NeverEncodeField.Should().Be(originalString);
        }

        [Fact]
        [ResetApplicationState]
        public async Task IgnoreAllModelWhenControllerActionIsDecoratedWithTheIgnoreAttribute()
        {
            string originalString = "<string>Hola Mundo</string>";
            string anotherString = "<string>Hola Mundo2</string>";

            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(originalString)
                .WithNeverEncodeField(anotherString)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelIgnoreAttibuteOnActionUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();

            firstModel.StringField.Should().Be(originalString);
            firstModel.NeverEncodeField.Should().Be(anotherString);
        }

        [Fact]
        [ResetApplicationState]
        public async Task IgnoreAllModelWhenControllerActionModelIsDecoratedWithTheIgnoreAttribute()
        {
            string originalString = "<string>Hola Mundo</string>";
            string anotherString = "<string>Hola Mundo2</string>";

            var request = new TypedModelBuilder()
                .WithInt(4)
                .WithString(originalString)
                .WithNeverEncodeField(anotherString)
                .Build();

            await Given.PostAndExpectOkAsync(ApiHelper.Post.PostModelIgnoreAttributeOnModelUrl(), request);

            var modelsInDb = Given.GetEncodedModels();

            modelsInDb.Should().NotBeNull().And.HaveCount(1);

            var firstModel = modelsInDb[0];
            firstModel.Should().NotBeNull();

            firstModel.StringField.Should().Be(originalString);
            firstModel.NeverEncodeField.Should().Be(anotherString);
        }

        public static class ApiHelper
        {
            public static class Post
            {
                public static string PostModelUrl() => "api/v1/encode/model";
                public static string PostModelIgnoreAttibuteOnActionUrl() => "api/v1/encode/model-ignore-all-attribute-on-action";
                public static string PostModelIgnoreAttributeOnModelUrl() => "api/v1/encode/model-ignore-all-attribute-on-root-model";
            }
        }
    }

}
