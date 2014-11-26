using System;

namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IApiConnection
    {
        TModel Get<TModel>(Uri link) where TModel : class, new();
        string Get();
        //Post, Patch, Put etc...
    }
}