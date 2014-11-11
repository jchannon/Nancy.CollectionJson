using System;
using System.Collections.Generic;
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
            var req = new RestRequest("/", Method.GET);
            var res = client.Execute(req).Content;
            try
            {
                var homeDoc = HomeDocument.Parse(res);
            }
            catch (Exception ex)
            {
                
            }
            return new Dictionary<string, string>();
        }
    }
}