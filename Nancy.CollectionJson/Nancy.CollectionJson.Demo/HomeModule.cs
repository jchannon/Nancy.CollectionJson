using System;
using Nancy.CollectionJson.Demo.Models;
using CollectionJson;
using System.Linq;

namespace Nancy.CollectionJson.Demo
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IFriendRepository repo)
        {
            Get["/{name}"] = parameters =>
            {
                var friends = repo.GetAll().Where(f => f.FullName.IndexOf(parameters.name, StringComparison.OrdinalIgnoreCase) > -1);
                
                return friends;
            };
        }
    }
}

