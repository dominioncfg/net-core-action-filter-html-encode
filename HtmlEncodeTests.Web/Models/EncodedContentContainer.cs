using System.Collections.Generic;

namespace HtmlEncodeTests.Web
{
    public class EncodedContentContainer : IEncodedContentContainer
    {
        private readonly List<TypedModel> _modelsContainer;
        private readonly List<Dictionary<string, string>> _dictsContainer;

        public EncodedContentContainer(List<TypedModel> container, List<Dictionary<string, string>> dictsContainer)
        {
            _modelsContainer = container;
            _dictsContainer = dictsContainer;
        }

        public void AddModel(TypedModel model) => _modelsContainer.Add(model);
        public void AddDict(Dictionary<string, string> dict) => _dictsContainer.Add(dict);
        public IEnumerable<TypedModel> GetAllModels() => _modelsContainer;
        public IEnumerable<Dictionary<string, string>> GetAllDicts() => _dictsContainer;
    }
}

