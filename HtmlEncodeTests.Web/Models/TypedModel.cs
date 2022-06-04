using HtmlEncodeTests.IntegrationTests.Encode;

namespace HtmlEncodeTests.Web
{
    public class TypedModel : RequestModelBase
    {
        //***To Make Sure readonly properties are ignored***
        public string ReadOnlyProperty { get { return "01"; } }
        public static string StaticReadOnlyProperty { get { return "02"; } }
        //***

        public string? StringField { get; set; } = string.Empty;
        public int IntField { get; set; }

        [NeverEncodeAsHtml]
        public string? NeverEncodeField { get; set; } = string.Empty;

        public InnerTypedModel? InnerModel { get; set; }
    }

    public class InnerTypedModel : RequestModelBase
    {
        public string InnerStringField { get; set; } = string.Empty;
        public int InnerIntField { get; set; }
    }

    [NeverEncodeAsHtml]
    public class IgnoreTypedModel : TypedModel { };
}

