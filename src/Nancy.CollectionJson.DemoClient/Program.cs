using System;
using Nancy.CollectionJson.DemoWrapper;
using Nancy.CollectionJson.Demo.Models;
using System.Threading;
using CollectionJson;

namespace Nancy.CollectionJson.DemoClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Connecting to server via wrapper...");
            Thread.Sleep(2500);
            //Friend should be in separate viewmodels library so server and client can both use it
            var client = new WrapperClient<Friend, Collection>();

            var data = client.List();

//            Console.WriteLine(data.ToJson());
//            Console.WriteLine("hyper");
            var hyperdata = client.ListHypermedia();

            Console.WriteLine("bef");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hyperdata));
            Console.WriteLine("aft");


            //  var x = hyperdata.FromJson<Collection>();
          //  var ff = Newtonsoft.Json.JsonConvert.DeserializeObject<Collection>(hyperdata);

            Console.ReadKey();
        }
    }
}
