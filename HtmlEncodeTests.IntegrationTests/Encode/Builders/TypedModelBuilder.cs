using HtmlEncodeTests.Web;
using System;

namespace HtmlEncodeTests.IntegrationTests
{
    public class TypedModelBuilder
    {
        private int intField;
        private string stringField;
        private string neverEncodeField;
        private string ignorePathProperty;
        private InnerTypedModel innerModelField;

        public TypedModelBuilder WithInt(int value)
        {
            intField = value;
            return this;
        }

        public TypedModelBuilder WithString(string value)
        {
            stringField = value;
            return this;
        }

        public TypedModelBuilder WithNeverEncodeField(string value)
        {
            neverEncodeField = value;
            return this;
        }

        public TypedModelBuilder WithIgnorePathProperty(string value)
        {
            ignorePathProperty = value;
            return this;
        }

        public TypedModelBuilder WithInnerModel(Action<InnerTypedModelBuilder> config)
        {
            var builder = new InnerTypedModelBuilder();
            config(builder);
            innerModelField = builder.Build();
            return this;
        }

        public TypedModel Build() => new()
        {
            IntField = intField,
            StringField = stringField,
            NeverEncodeField = neverEncodeField,
            IgnorePathProperty = ignorePathProperty,
            InnerModel = innerModelField,
        };
    }

    public class InnerTypedModelBuilder
    {
        private int intField;
        private string stringField;
        private string ignorePathProperty;

        public InnerTypedModelBuilder WithInt(int value)
        {
            intField = value;
            return this;
        }

        public InnerTypedModelBuilder WithString(string value)
        {
            stringField = value;
            return this;
        }

        public InnerTypedModelBuilder WithIgnorePathProperty(string value)
        {
            ignorePathProperty = value;
            return this;
        }

        public InnerTypedModel Build() => new()
        {
            InnerIntField = intField,
            InnerStringField = stringField,
            IgnorePathProperty = ignorePathProperty,
        };
    }

}
