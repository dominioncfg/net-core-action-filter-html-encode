using HtmlEncodeTests.Web;
using System.Collections.Generic;

namespace HtmlEncodeTests.IntegrationTests
{
    public class EncodeDictBuilder
    {
        private readonly Dictionary<string, string> fields = new();

        public EncodeDictBuilder WithString(string key, string value)
        {
            fields.Add(key, value);
            return this;
        }

        public Dictionary<string,string> Build() => fields;
    }

}
