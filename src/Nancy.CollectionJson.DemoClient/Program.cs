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
            //Let server fire up
            Thread.Sleep(1500);

            //Friend should be in separate viewmodels library so server and client can both use it
            var client = new WrapperClient<Friend, Collection>(new LinkLookup(new HomeDocProvider()));

            var friendList = client.List();
            //Console.WriteLine("We have " + friendList.Count + " friends!");

            //var friend = client.GetHypermedia(1);

            //friend. = "billynomates@gmail.com";

            //Console.WriteLine("Lets change " + friend.FullName + " email address");

            //var statuscode = client.Save(friend.Id, friend);

            //Console.WriteLine("Saved! Status Code: "+statuscode);

            var hyperdata = client.ListHypermedia();

            //Console.WriteLine("bef");
            //Console.WriteLine(hyperdata.ToJson());
            //Console.WriteLine("aft");


      

            Console.ReadKey();
        }
    }
}
