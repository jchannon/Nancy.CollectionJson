using System;
using Nancy.CollectionJson.Demo.Models;
using CollectionJson;
using System.Linq;
using System.Collections.Generic;
using Nancy.ModelBinding;

namespace Nancy.CollectionJson.Demo
{
    public class FriendsModule : NancyModule
    {
        public FriendsModule(IFriendRepository repo, ICollectionJsonDocumentReader<Friend> friendReader)
            : base("/friends")
        {
            Get["/"] = _ =>
            {
                var friends = repo.GetAll(); 
                return friends;
            };

            Get["/{id:int}"] = parameters =>
            {
                int id = parameters.id;
                return repo.Get(id);
            };

            Post["/"] = _ =>
            {
                var data = this.Bind<List<Data>>(); 

                var doc = new WriteDocument(){ Template = new Template(){ Data = data } };
                   
                var friend = friendReader.Read(doc);
                
                var id = repo.Add(friend);

                return Negotiate.WithHeader("Location", this.Request.Url.ToString() + "/" + id).WithStatusCode(HttpStatusCode.Created);
            };

            Put["/{id:int}"] = parameters =>
            {
                int id = parameters.id;

                var data = this.Bind<List<Data>>(); 

                var doc = new WriteDocument(){ Template = new Template(){ Data = data } };

                var friend = friendReader.Read(doc);
                friend.Id = id;
                repo.Update(friend);

                return friend;
            };

            Delete["/{id:int}"] = parameters =>
            {
                int id = parameters.id;
                repo.Remove(id);

                return HttpStatusCode.NoContent;
            };

            Get["/search/{name}"] = parameters =>
            {
                var friends = repo.Search(parameters.name);
                return friends;
            };
        }
    }
}

