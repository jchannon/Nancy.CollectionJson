using System.Collections.Concurrent;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class LinkLookup : ILinkLookup
    {
        private readonly IHomeDocProvider homeDocProvider;
        private readonly ConcurrentDictionary<string, string> cachedValues = new ConcurrentDictionary<string, string>();

        public LinkLookup(IHomeDocProvider homeDocProvider)
        {
            this.homeDocProvider = homeDocProvider;
        }

        public string GetLink(string rel)
        {
            string link;
            if (!cachedValues.TryGetValue(rel, out link))
            {
                PopulateCacheFromHomeDoc();
                if (!cachedValues.TryGetValue(rel, out link))
                {
                    //throw or traverse UP somehow eg/
                }
            }

            return link;
        }

        private void PopulateCacheFromHomeDoc()
        {
            var homedocValues = homeDocProvider.GetLinks();
            foreach (var relLink in homedocValues)
            {
                cachedValues.TryAdd(relLink.Key, relLink.Value);
            }
        }
    }
}