using System;

namespace Nancy.CollectionJson.Demo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                var model = new {Name = "John"};
                return model;
            };
        }
    }
}

