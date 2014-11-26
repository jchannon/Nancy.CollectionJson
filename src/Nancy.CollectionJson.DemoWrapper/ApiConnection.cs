using System;
using RestSharp;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class ApiConnection : IApiConnection
    {
        private readonly RestClient client;
        private const string ROOT = "http://localhost:9200";

        public ApiConnection()
        {
            client = new RestClient();
            client.AddHandler("application/vnd.collection+json", new RestSharpServiceStackSerializer());
        }

        public TModel Get<TModel>(Uri link) where TModel : class, new()
        {
            client.BaseUrl = new Uri(link.Scheme + "://" + link.Host + ":" + link.Port);

            var req = new RestRequest(link.AbsolutePath, Method.GET);

            req.AddHeader("Accept", "application/vnd.collection+json");

            var resp = client.Execute<TModel>(req);

            return resp.Data;
        }

        public string Get()
        {
            client.BaseUrl = new Uri(ROOT);

            var req = new RestRequest("/", Method.GET);

            var resp = client.Execute(req);

            return resp.Content;
        }
    }
}