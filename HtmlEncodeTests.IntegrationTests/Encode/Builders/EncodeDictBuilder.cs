using HtmlEncodeTests.Web;
using System.Collections.Generic;

namespace HtmlEncodeTests.IntegrationTests
{
    public class EncodeDictBuilder
    {
        private readonly Dictionary<string, object> fields = new();

        public EncodeDictBuilder WithIntField(string key, int value)
        {
            fields.Add(key, value);
            return this;
        }

        public EncodeDictBuilder WithString(string key, string value)
        {
            fields.Add(key, value);
            return this;
        }

        public Dictionary<string,object> Build() => fields;
    }

}
