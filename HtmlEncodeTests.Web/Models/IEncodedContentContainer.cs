using System.Collections.Generic;

namespace HtmlEncodeTests.Web
{
    public interface IEncodedContentContainer
    {
        public void AddModel(TypedModel model);
        public IEnumerable<TypedModel> GetAllModels();
    }
}

