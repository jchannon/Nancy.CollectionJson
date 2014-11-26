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

            var apiConn = new ApiConnection();

         
            var client = new RootClient(new HomeDocProvider(apiConn), apiConn);
            client.Connect();

            var allFriendCollection = client.FriendsClient.List();
            Console.WriteLine(allFriendCollection.Items.Count + " friends");

            //this should throw an exception as we haven't followed the "link tree" to be able to search. (in reality search should be available up a level, I'm just using
            //it as a method to demonstrate context specific actions
            try
            {
                var contextspecificdata = client.FriendsClient.FriendClient.Search("michael");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            var friendCollection = client.FriendsClient.Get(1);

            //Now we should be able to do context specific things on a friend now we have made a req to a friend which will have returned context specific links
             var validcontextspecificdata = client.FriendsClient.FriendClient.Search("michael");
            Console.WriteLine(validcontextspecificdata.Items.Count + " friends found");

            Console.ReadKey();
        }
    }
}
