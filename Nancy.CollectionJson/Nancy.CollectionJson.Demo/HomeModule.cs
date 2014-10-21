using System;
using Nancy.CollectionJson.Demo.Models;
using CollectionJson;
using System.Linq;
using System.Collections.Generic;
using Nancy.ModelBinding;

namespace Nancy.CollectionJson.Demo
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IFriendRepository repo, ICollectionJsonDocumentReader<Friend> friendReader)
        {           
            Get["/search/{name}"] = parameters =>
            {
                var friends = repo.GetAll().Where(f => f.FullName.IndexOf(parameters.name, StringComparison.OrdinalIgnoreCase) > -1);
                return friends;
            };

            Post["/"] = _ =>
            {
                var data = this.Bind<List<Data>>(); //Nancy doesn't bind to List<T> located 3 levels deep on an object
                
                //This doesn't compile as Writedocument properties are private set
                var doc = new WriteDocument();
                doc.Template = new Template();
                doc.Template.Data = data;
                   
                var friend = friendReader.Read(doc);
                var id = repo.Add(friend);
                this.Context.Response.Headers.Add("Location", "http://mydomain.com/friends/" + id);
                return HttpStatusCode.Created;
             
            };
        }
    }
}

