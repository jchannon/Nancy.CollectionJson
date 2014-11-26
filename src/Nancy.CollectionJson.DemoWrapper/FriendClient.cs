using System;
using System.Collections.Generic;
using System.Linq;
using CollectionJson;
using Tavis.UriTemplates;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class FriendClient : IFriendClient
    {
        private readonly IDictionary<string, Uri> linksDictionary;
        private readonly IApiConnection apiConnection;

        public FriendClient(IDictionary<string, Uri> linksDictionary, IApiConnection apiConnection)
        {
            this.linksDictionary = linksDictionary;
            this.apiConnection = apiConnection;
        }

        public Collection Search(string friendName)
        {
            if (linksDictionary.Count == 0)
            {
                throw new ArgumentException("Need to get a friend before accessing friend specific methods");
            }

            //We know the REL from documentation etc
            var link = this.linksDictionary.FirstOrDefault(x => x.Key == "search").Value;

            var temp = new UriTemplate(link.OriginalString);
            temp.SetParameter("searchname", friendName);

            var resolvedlink = temp.Resolve();
            link = new Uri(resolvedlink);

            var results = apiConnection.Get<Collection>(link);

            return results;
        }
    }
}