using System.Collections.Generic;

namespace HtmlEncodeTests.Web
{
    public interface IEncodedContentContainer
    {
        public void AddModel(TypedModel model);
        public void AddDict(Dictionary<string, string> dict);
        public IEnumerable<TypedModel> GetAllModels();
        public IEnumerable<Dictionary<string, string>> GetAllDicts();
    }
}

