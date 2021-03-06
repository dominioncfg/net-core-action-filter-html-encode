using HtmlEncodeTests.IntegrationTests.Encode;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace HtmlEncodeTests.Web
{

    [ApiController]
    [Route("api/v1/encode")]
    public class EncodeController : ControllerBase
    {
        private readonly IEncodedContentContainer _encodedContent;
        public EncodeController(IEncodedContentContainer encodedContent)
        {
            _encodedContent = encodedContent;
        }

        [HttpPost("model")]
        public void PostModel([FromBody] TypedModel model, CancellationToken _)
        {
            _encodedContent.AddModel(model);
        }

        [HttpPost("model-ignore-all-attribute-on-action")]
        [NeverEncodeAsHtml]
        public void PostModelIgnoreAction([FromBody] TypedModel model, CancellationToken _)
        {
            _encodedContent.AddModel(model);
        }

        [HttpPost("model-ignore-all-attribute-on-root-model")]
        public void PostModelIgnoreModel([FromBody] IgnoreTypedModel model, CancellationToken _)
        {
            _encodedContent.AddModel(model);
        }
    }
}

