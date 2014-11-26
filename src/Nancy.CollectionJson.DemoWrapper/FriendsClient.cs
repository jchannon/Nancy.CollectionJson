using System;
using System.Collections.Generic;
using CollectionJson;
using Nancy.CollectionJson.Demo.ViewModels;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class FriendsClient : IFriendsClient
    {
        private readonly IDictionary<string, Uri> linksDictionary;
        private readonly IDictionary<string, Uri> friendDictionary;

        private readonly IApiConnection apiConnection;

        public FriendsClient(IDictionary<string, Uri> linksDictionary, IApiConnection apiConnection)
        {
            this.friendDictionary = new Dictionary<string, Uri>();

            this.linksDictionary = linksDictionary;
            this.apiConnection = apiConnection;
        }

        public Collection List()
        {
            var link = this.linksDictionary["http://localhost:9200/friends"];

            var data = apiConnection.Get<Collection>(link);

            return data;
        }

        public IFriendClient FriendClient
        {
            get
            {
                //Not sure what to do about dependency injection if we had more than one property
                return new FriendClient(this.friendDictionary, this.apiConnection);
            }
        }

        public Collection Get(int id)
        {
            var link = this.linksDictionary["http://localhost:9200/friends"];

            var collection = apiConnection.Get<Collection>(link);

            //This could be Links collection as well as Queries
            foreach (var item in collection.Queries)
            {
                friendDictionary.Add(item.Rel, item.Href);
            }

            return collection;


        }
    }
}