using System;
using System.Collections.Generic;
using RestSharp;

namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IWrapperClient<T,THyperMediaModel> where T:class, new() where THyperMediaModel:class, new()
    {
        T Get(int id);

        List<T> List(string acceptHeader = "application/json");

        THyperMediaModel ListHypermedia(string acceptHeader = "application/vnd.collection+json");

        THyperMediaModel GetHypermedia(int id, string acceptHeader = "application/vnd.collection+json");
    }



}

