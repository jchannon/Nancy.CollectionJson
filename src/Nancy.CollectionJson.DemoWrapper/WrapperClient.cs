using System;
using System.Collections.Generic;
using RestSharp;
using Nancy.CollectionJson.Demo.Models;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class WrapperClient<T,U> : IWrapperClient<T,U> where T:class, new() where U:class, new()
    {
        private RestClient client;

        public WrapperClient()
        {
            client = new RestClient("http://localhost:9200");
        }

        public T Get(int id)
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/" + id, Method.GET);

            var res = client.Execute<T>(req);
            return res.Data;
        }

        public List<T> List(string acceptHeader = "application/json")
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/", Method.GET);
           
            req.AddHeader("Accept", acceptHeader);
            
            var res = client.Execute<List<T>>(req);
            return res.Data;
        }


        public U ListHypermedia(string acceptHeader = "application/vnd.collection+json")
        {
            //the url shouldnt be hardcoded but not sure how to resolve this
            var req = new RestRequest("/friends/", Method.GET);

            req.AddHeader("Accept", acceptHeader);

            var res = client.Execute<U>(req);



            return res.Data;
        }
    }


}

