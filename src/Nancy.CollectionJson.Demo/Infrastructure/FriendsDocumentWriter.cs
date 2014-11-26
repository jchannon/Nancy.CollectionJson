﻿using CollectionJson;
using System;
using System.Collections.Generic;
using Nancy;
using Nancy.CollectionJson.Demo.Models;
using Nancy.CollectionJson.Demo.ViewModels;

namespace Nancy.CollectionJson.Demo.Infrastructure
{
    public class FriendsDocumentWriter : CollectionJsonDocumentWriter<Friend>
    {
        public override IReadDocument Write(IEnumerable<Friend> friends, Uri uri)
        {
            var document = new ReadDocument();
            var collection = new Collection { Version = "1.0", Href = new Uri(uri, "/friends/") };
            document.Collection = collection;

            collection.Links.Add(new Link { Rel = "Feed", Href = new Uri(uri, "/friends/rss") });

            foreach (var friend in friends)
            {
                var item = new Item { Href = new Uri(uri, "/friends/" + friend.Id) };
                item.Extensions().Model = "friend";

                item.Data.Add(new Data { Name = "full-name", Value = friend.FullName, Prompt = "Full Name" });
                item.Data.Add(new Data { Name = "email", Value = friend.Email, Prompt = "Email" });
                item.Data.Add(new Data{ Name = "short-name", Value = friend.ShortName, Prompt = "Short Name" });
                if (friend.Blog != null)
                {
                    item.Links.Add(new Link { Rel = "blog", Href = friend.Blog, Prompt = "Blog" });
                }
                item.Links.Add(new Link { Rel = "avatar", Href = friend.Avatar, Prompt = "Avatar", Render = "Image" });
                collection.Items.Add(item);
            }

            var query = new Query { Rel = "search", Href = new Uri(uri, "/friends/search/{searchname}"), Prompt = "Search" };
            query.Data.Add(new Data { Name = "name", Prompt = "Value to match against the Full Name" });
            collection.Queries.Add(query);

            var data = collection.Template.Data;
            data.Add(new Data { Name = "name", Prompt = "Full Name" });
            data.Add(new Data { Name = "email", Prompt = "Email" });
            data.Add(new Data { Name = "blog", Prompt = "Blog" });
            data.Add(new Data { Name = "avatar", Prompt = "Avatar" });
            return document;
        }
    }

     
}
