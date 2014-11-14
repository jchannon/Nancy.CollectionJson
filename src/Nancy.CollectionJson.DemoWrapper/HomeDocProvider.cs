using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using Tavis.Home;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class HomeDocProvider : IHomeDocProvider
    {
        public IDictionary<string, string> GetLinks()
        {
            var client = new RestClient("http://localhost:9200");
            // client.AddHandler("application/home+json", new RestSharpServiceStackSerializer());
            var req = new RestRequest(Method.GET);
            var res = client.Execute(req);
            var data = res.Content;
            var dic = new Dictionary<string, string>();
            var homeDoc = HomeDocument.Parse(data);
            foreach (var resource in homeDoc.Resources)
            {
                dic.Add(resource.Relation, resource.Target.OriginalString);
            }
            return dic;
        }
    }
}