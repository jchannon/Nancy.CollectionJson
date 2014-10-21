﻿using System;
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

                return Negotiate.WithHeader("Location", "http://mydomain.com/friends/" + id).WithStatusCode(HttpStatusCode.Created);
            };
        }
    }
}

