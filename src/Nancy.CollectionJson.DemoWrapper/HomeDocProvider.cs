using System;
using System.Collections.Generic;
using Tavis.Home;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class HomeDocProvider : IHomeDocProvider
    {
        private readonly IApiConnection apiConnection;

        public HomeDocProvider(IApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        public IDictionary<string, Uri> GetLinks()
        {
            var data = this.apiConnection.Get();

            var dic = new Dictionary<string, Uri>();
            
            var homeDoc = HomeDocument.Parse(data);
            
            foreach (var resource in homeDoc.Resources)
            {
                dic.Add(resource.Relation, resource.Target);
            }
            
            return dic;
        }
    }
}