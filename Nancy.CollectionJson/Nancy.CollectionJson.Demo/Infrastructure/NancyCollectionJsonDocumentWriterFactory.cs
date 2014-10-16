using System;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;
using System.Linq;
using CollectionJson;
using Nancy.CollectionJson.Demo.Infrastructure;
using Nancy.CollectionJson.Demo.Models;

namespace Nancy.CollectionJson.Demo.Infrastructure
{

    public class NancyCollectionJsonDocumentWriterFactory : ICollectionJsonDocumentWriterFactory
    {
        public Collection Get(object model, NancyContext ctx)
        {
            //reflection here on model to work out the 'writer' to use ?
            var doc = new FriendDocumentWriter(ctx);
            var blah = doc.Write((IEnumerable<Friend>)model);
            return blah.Collection;
        }
    }
}
