using System;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;
using System.Linq;
using CollectionJson;
using Nancy.CollectionJson.Demo.Infrastructure;
using Nancy.CollectionJson.Demo.Models;

namespace Nancy.CollectionJson.Demo.Infrastructure
{
    public class FriendLinkGenerator : ILinkGenerator
    {
        public bool CanHandle(Type model)
        {
            var expectedType = typeof(IEnumerable<Friend>);

            if (expectedType.IsAssignableFrom(model))
            {
                return true;
            }
              
            return false;
        }

        public Collection Handle(object model, NancyContext context)
        {
            var doc = new FriendDocumentWriter(context);
            var blah = doc.Write((IEnumerable<Friend>)model);
            return blah.Collection;
        }
    }
}
