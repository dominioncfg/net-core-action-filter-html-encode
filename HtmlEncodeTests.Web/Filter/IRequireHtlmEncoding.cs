using System;
using System.Collections.Generic;

namespace HtmlEncodeTests.IntegrationTests.Encode
{
    public interface IRequireHtlmEncoding { }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NeverEncodeAsHtmlAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreEncodeAsHtmlAttribute : Attribute {

        public string[] Fields { get; set; } = Array.Empty<string>();

        public IgnoreEncodeAsHtmlAttribute(params string[] fields)
        {
            Fields = fields;
        }
    }

}
