using System;

namespace HtmlEncodeTests.IntegrationTests.Encode
{
    public interface IRequireHtlmEncoding { }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NeverEncodeAsHtmlAttribute : Attribute { }   
}
