using System;
using Nancy.CollectionJson.Demo.Models;
using Nancy.CollectionJson.Demo.Infrastructure;
using CollectionJson;

namespace Nancy.CollectionJson.Demo
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
        }

        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
        }
    }
}

