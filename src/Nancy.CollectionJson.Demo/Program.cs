using System;
using Nancy.Hosting.Self;

namespace Nancy.CollectionJson.Demo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:9200")))
            {
                host.Start();
                Console.WriteLine("Server running on http://localhost:9200");
                Console.ReadLine();
            }
        }
    }
}
