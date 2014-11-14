using System;
using Nancy.CollectionJson.Demo.ViewModels;
using Nancy.CollectionJson.DemoWrapper;
using System.Threading;
using CollectionJson;


namespace Nancy.CollectionJson.DemoClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //http://codereview.stackexchange.com/questions/16493
            Console.WriteLine("Connecting to server via wrapper...");

            //Let the HTTP server fire up
            Thread.Sleep(1500);

            var client = new WrapperClient<Friend, Collection>(new LinkLookup(new HomeDocProvider()));

            var friendList = client.List();

            var friend = client.Get(1);

            Console.WriteLine(friend.Email);
            
            friend.Email = "changedemail@home.com";
            client.Save(1, friend);
            
            var savedfriend = client.Get(1);
            Console.WriteLine(savedfriend.Email);

            var collectionJsonList = client.ListHypermedia();
            var collectionJsonListWithOneFriend = client.GetHypermedia(1);

            Console.ReadKey();
        }
    }
}
