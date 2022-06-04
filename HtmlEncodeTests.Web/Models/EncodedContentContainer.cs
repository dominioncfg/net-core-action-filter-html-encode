using System.Collections.Generic;

namespace HtmlEncodeTests.Web
{
    public class EncodedContentContainer : IEncodedContentContainer
    {
        private readonly List<TypedModel> _modelsContainer;

        public EncodedContentContainer(List<TypedModel> container)
        {
            _modelsContainer = container;
        }

        public void AddModel(TypedModel model) => _modelsContainer.Add(model);
        public IEnumerable<TypedModel> GetAllModels() => _modelsContainer;
    }
}

