using System;
using Nancy.CollectionJson.Demo.Models;
using CollectionJson;
using System.Linq;
using System.Collections.Generic;

namespace Nancy.CollectionJson.Demo
{
    public class HomeModule : NancyModule
    {
        IEnumerable<ILinkGenerator> thelinks;

        public HomeModule(IFriendRepository repo, IEnumerable<ILinkGenerator> thelinks)
        {
            this.thelinks = thelinks;
           
            Get["/{name}"] = parameters =>
            {
                var friends = repo.GetAll().Where(f => f.FullName.IndexOf(parameters.name, StringComparison.OrdinalIgnoreCase) > -1);
                Console.Write(this.thelinks.GetHashCode().ToString());
                return friends;
            };
        }
    }
}

