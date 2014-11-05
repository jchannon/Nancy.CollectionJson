using System;
using System.Collections.Generic;
using RestSharp;

namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IWrapperClient<T,U> where T:class, new() where U:class, new()
    {
        T Get(int id);

        List<T> List(string acceptHeader = "application/json");

        U ListHypermedia(string acceptHeader = "application/vnd.collection+json");
    }



}

