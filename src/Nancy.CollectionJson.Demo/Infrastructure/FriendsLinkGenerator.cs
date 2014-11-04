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
        private readonly CollectionJsonDocumentWriter<Friend> docWriter;

        public FriendsLinkGenerator(CollectionJsonDocumentWriter<Friend> docWriter)
        {
            this.docWriter = docWriter;
        }

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

        public Collection Handle(object model, Uri uri)
        {
            var modeldata = model as IEnumerable<Friend>;
            if (modeldata == null)
            {
                var oneFriend = (Friend)model;
                var ber = docWriter.Write(oneFriend, uri);
                return ber.Collection;
            }

            var blah = docWriter.Write(modeldata, uri);
            return blah.Collection;
        }
    }
}
