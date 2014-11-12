using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using RestSharp;
using RestSharp.Serializers;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class WrapperClient<TModel, THyperMediaModel> : IWrapperClient<TModel, THyperMediaModel>
        where TModel : class, new()
        where THyperMediaModel : class, new()
    {
        private readonly ILinkLookup linkLookup;
        private RestClient client;

        public WrapperClient(ILinkLookup linkLookup)
        {
            this.linkLookup = linkLookup;

            //client = new RestClient("http://localhost:9200");
           // client.AddHandler("application/vnd.collection+json", new RestSharpServiceStackSerializer());
        }

        public TModel Get(int id)
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/" + id, Method.GET);

            var res = client.Execute<TModel>(req);
            return res.Data;
        }

        public List<TModel> List(string acceptHeader = "application/json")
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var link = this.linkLookup.GetLink("http://localhost:9200/friends");


            return new List<TModel>();
        }


        public int Save(int id, TModel model)
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/" + id, Method.PUT);
            req.AddJsonBody(model);
            var res = client.Execute<TModel>(req);
            return (int)res.StatusCode;
        }

        public THyperMediaModel ListHypermedia(string acceptHeader = "application/vnd.collection+json")
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/", Method.GET);

            req.AddHeader("Accept", acceptHeader);

            var res = client.Execute<THyperMediaModel>(req);

            return res.Data;
        }

        public THyperMediaModel GetHypermedia(int id, string acceptHeader = "application/vnd.collection+json")
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/" + id, Method.GET);
            req.AddHeader("Accept", acceptHeader);
            var res = client.Execute<THyperMediaModel>(req);
            return res.Data;
        }
    }
}

