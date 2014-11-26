using System;
using System.Collections.Generic;

namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IHomeDocProvider
    {
        IDictionary<string, Uri> GetLinks();
    }
}