
using System;
using System.Net.Http;
using Tavis;
using Tavis.Home;
using Tavis.IANA;

namespace Nancy.CollectionJson.Demo
{
    public class HomeModule : NancyModule
    {
        public const string Root = "http://localhost:9200";
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                var home = new HomeDocument();

                var allfriendslink = new Link()
                {
                    Relation = Root + "/friends",
                    Target = new Uri(Root + "/friends")
                };

                allfriendslink.AddHint<AllowHint>(h =>
                {
                    h.AddMethod(HttpMethod.Get);
                    h.AddMethod(HttpMethod.Post);
                });

                AddMediaTypes(allfriendslink);

                home.AddResource((Link)allfriendslink);

                var friendLink = new Link()
                {
                    Relation = Root + "/friend",
                    Target = new Uri(Root + "/friends/{id}")
                };

                friendLink.AddHint<AllowHint>(h =>
                {
                    h.AddMethod(HttpMethod.Get);
                    h.AddMethod(HttpMethod.Put);
                    h.AddMethod(HttpMethod.Delete);
                });

                AddMediaTypes(friendLink);

                home.AddResource(friendLink);

                var searchLink = new Link()
                {
                    Relation = Root + "/search",
                    Target = new Uri(Root + "/friends/search/{name}")
                };

                searchLink.AddHint<AllowHint>(h =>
                {
                    h.AddMethod(HttpMethod.Get);
                });

                AddMediaTypes(searchLink);

                home.AddResource(searchLink);

                return Response.AsHomeDocument(home);
            };
        }

        private void AddMediaTypes(Link link)
        {
            link.AddHint<FormatsHint>(h =>
            {
                h.AddMediaType("application/json");
                h.AddMediaType("application/xml");
                h.AddMediaType("application/vnd.issue+json");
            });
        }
    }

    public class TavisHomeResponse : Response
    {
        public TavisHomeResponse(HomeDocument homeDocument)
        {
            this.ContentType = "application/home+json";

            this.Contents = stream => homeDocument.Save(stream);
        }
    }

    public static class NancyResponseExtensions
    {
        public static Response AsHomeDocument(this IResponseFormatter formatter, HomeDocument homeDocument)
        {
            return new TavisHomeResponse(homeDocument);
        }
    }
}
