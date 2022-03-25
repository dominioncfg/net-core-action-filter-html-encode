using System.Reflection;
using Xunit.Sdk;

namespace HtmlEncodeTests.IntegrationTests
{
    public class ResetApplicationStateAttribute : BeforeAfterTestAttribute
    {
        public override void After(MethodInfo methodUnderTest) => base.After(methodUnderTest);
        public override void Before(MethodInfo methodUnderTest) => TestServerFixture.OnTestInitResetApplicationState();
    }
}
