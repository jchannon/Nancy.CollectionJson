using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using RestSharp;
using RestSharp.Serializers;
using Tavis.UriTemplates;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class WrapperClient<TModel, THyperMediaModel> : IWrapperClient<TModel, THyperMediaModel>
        where TModel : class, new()
        where THyperMediaModel : class, new()
    {
        private readonly ILinkLookup linkLookup;
        private readonly RestClient client;
        private const string ROOT = "http://localhost:9200";

        public WrapperClient(ILinkLookup linkLookup)
        {
            this.linkLookup = linkLookup;

            client = new RestClient("http://localhost:9200");
            client.AddHandler("application/vnd.collection+json", new RestSharpServiceStackSerializer());
        }

        public TModel Get(int id, string acceptHeader = "application/json")
        {
            var link = this.linkLookup.GetLink("http://localhost:9200/friend");

            var template = new UriTemplate(link);

            //as this is a wrapper we know the parameters for our interface eg/id but if we add a new one the template will still get populated via
            //the enumeration but we'd need to add new ones to the method args. Dumb clients would need to read docs to know what to assign to each
            //possible parameter
            foreach (var parameterName in template.GetParameterNames())
            {
                template.SetParameter(parameterName, id);
            }

            var uriString = template.Resolve();

            var req = new RestRequest(uriString, Method.GET);
            
            req.AddHeader("Accept", acceptHeader);

            var res = client.Execute<TModel>(req);

            return res.Data;
        }

        public List<TModel> List(string acceptHeader = "application/json")
        {
            var link = this.linkLookup.GetLink("http://localhost:9200/friends");
            var req = new RestRequest(link, Method.GET);
            req.AddHeader("Accept", acceptHeader);

            var res = client.Execute<List<TModel>>(req);

            //Context specific rel then client.GetById(1).ContextSpecificMethod

            return res.Data;
        }


        public int Save(int id, TModel model)
        {
            var link = this.linkLookup.GetLink("http://localhost:9200/friend");

            var template = new UriTemplate(link);

            foreach (var parameterName in template.GetParameterNames())
            {
                template.SetParameter(parameterName, id);
            }

            var uriString = template.Resolve();

            var values = new List<dynamic>();
            var propertyInfo = typeof(TModel).GetProperties();

            //data template needs to be stored in a cache too i assume on a dumb client so they know what to populate and what the payload will look like
            values.Add(new { name = "name", value = propertyInfo.FirstOrDefault(x => x.Name.ToLower() == "fullname").GetValue(model, null) });
            values.Add(new { name = "email", value = propertyInfo.FirstOrDefault(x => x.Name.ToLower() == "email").GetValue(model, null) });
            values.Add(new { name = "blog", value = propertyInfo.FirstOrDefault(x => x.Name.ToLower() == "blog").GetValue(model, null) });
            values.Add(new { name = "avatar", value = propertyInfo.FirstOrDefault(x => x.Name.ToLower() == "avatar").GetValue(model, null) });

            var req = new RestRequest(uriString, Method.PUT);

            req.AddJsonBody(values);

            var res = client.Execute<TModel>(req);

            return (int)res.StatusCode;
        }

        public THyperMediaModel ListHypermedia(string acceptHeader = "application/vnd.collection+json")
        {
            var link = this.linkLookup.GetLink("http://localhost:9200/friends");
            var req = new RestRequest(link, Method.GET);
            req.AddHeader("Accept", acceptHeader);

            var res = client.Execute<THyperMediaModel>(req);

            return res.Data;
        }

        public THyperMediaModel GetHypermedia(int id, string acceptHeader = "application/vnd.collection+json")
        {
            var link = this.linkLookup.GetLink("http://localhost:9200/friend");

            var template = new UriTemplate(link);

            //as this is a wrapper we know the parameters for our interface eg/id but if we add a new one the template will still get populated via
            //the enumeration but we'd need to add new ones to the method args. Dumb clients would need to read docs to know what to assign to each
            //possible parameter
            foreach (var parameterName in template.GetParameterNames())
            {
                template.SetParameter(parameterName, id);
            }

            var uriString = template.Resolve();

            var req = new RestRequest(uriString, Method.GET);

            req.AddHeader("Accept", acceptHeader);
            var res = client.Execute<THyperMediaModel>(req);
            return res.Data;
        }
    }
}

