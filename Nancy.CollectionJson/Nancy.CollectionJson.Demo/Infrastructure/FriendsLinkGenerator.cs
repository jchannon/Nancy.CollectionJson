using System;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;
using System.Linq;
using CollectionJson;
using Nancy.CollectionJson.Demo.Infrastructure;
using Nancy.CollectionJson.Demo.Models;

namespace Nancy.CollectionJson.Demo.Infrastructure
{
    public class FriendsLinkGenerator : ILinkGenerator
    {
        public bool CanHandle(Type model)
        {
            var expectedType = typeof(IEnumerable<Friend>);

            if (expectedType.IsAssignableFrom(model))
            {
                return true;
            }

            if (model == typeof(Friend))
            {
                return true;
            }
              
            return false;
        }

        public Collection Handle(object model, NancyContext context)
        {
            var doc = new FriendsDocumentWriter(context);
            var modeldata = model as IEnumerable<Friend>;
            if (modeldata == null)
            {
                var oneFriend = (Friend)model;
                var ber = doc.Write(oneFriend);
                return ber.Collection;
            }

            var blah = doc.Write(modeldata);
            return blah.Collection;
        }
    }
}
