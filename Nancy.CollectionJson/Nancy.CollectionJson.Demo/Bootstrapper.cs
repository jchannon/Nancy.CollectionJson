using System;
using Nancy.CollectionJson.Demo.Models;
using Nancy.CollectionJson.Demo.Infrastructure;
using CollectionJson;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;
using Nancy.TinyIoc;

namespace Nancy.CollectionJson.Demo
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.DisableErrorTraces = false;
        }

        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<ICollectionJsonDocumentReader<Friend>, FriendDocumentReader>();

            container.Register<CollectionJsonDocumentWriter<Friend>, FriendsDocumentWriter>();

            container.RegisterMultiple<ILinkGenerator>(new[]{ typeof(FriendsLinkGenerator) });
        }



        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

        }
    }
}

