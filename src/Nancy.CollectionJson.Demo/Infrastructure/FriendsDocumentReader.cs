using CollectionJson;
using System;
using Nancy.CollectionJson.Demo.Models;
using Nancy.CollectionJson.Demo.ViewModels;

namespace Nancy.CollectionJson.Demo.Infrastructure
{
    public class FriendsDocumentReader : ICollectionJsonDocumentReader<Friend>
    {
        public Friend Read(IWriteDocument document)
        {
            var template = document.Template;
            var friend = new Friend();
            friend.FullName = template.Data.GetDataByName("name").Value;
            friend.Email = template.Data.GetDataByName("email").Value;
            friend.Blog = new Uri(template.Data.GetDataByName("blog").Value);
            friend.Avatar = new Uri(template.Data.GetDataByName("avatar").Value);
            return friend;
        }
    }

}
