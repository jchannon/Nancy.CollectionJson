
using System;
using System.Net.Http;
using Tavis;
using Tavis.Home;
using Tavis.IANA;

namespace Nancy.CollectionJson.Demo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                var home = new HomeDocument();

                //home.AddResource<AboutLink>(l =>
                //{
                //    l.Target = new Uri("about", UriKind.Relative);
                //    l.AddHint<AllowHint>(h => h.AddMethod(HttpMethod.Get));
                //    l.AddHint<FormatsHint>(h => h.AddMediaType("application/json"));
                //    l.AddHint<AcceptPostHint>(h => h.AddMediaType("application/vnd.tavis.foo+json"));
                //    l.AddHint<AcceptPreferHint>(h => h.AddPreference("handling"));
                //});

                //home.AddResource<Link>(l =>
                //{
                //    l.Relation = "http://localhost:9200";
                //    l.Target = new Uri("/", UriKind.Relative);
                //});

                var link = new Link() { Relation = "http://localhost:9200/profile#", Target = new Uri("/issue/{id}", UriKind.Relative) };
                link.AddHint<AllowHint>(h => h.AddMethod(HttpMethod.Get));
                link.AddHint<FormatsHint>(h =>
                {
                    h.AddMediaType("application/json");
                    h.AddMediaType("application/vnd.issue+json");
                });
                home.AddResource((Link)link);

                return Negotiate.WithModel(home).WithContentType("application/home+json");

                
            };
        }
    }
}
