using System;
using CollectionJson;

namespace Nancy.CollectionJson
{
    public interface ILinkGenerator
    {
        bool CanHandle(Type model);

        Collection Handle(object model, NancyContext context);
    }
}