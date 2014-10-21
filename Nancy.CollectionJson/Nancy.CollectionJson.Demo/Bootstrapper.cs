using System;
using Nancy.CollectionJson.Demo.Models;
using Nancy.CollectionJson.Demo.Infrastructure;
using CollectionJson;

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

            container.RegisterMultiple<ILinkGenerator>(new[]{ typeof(FriendLinkGenerator) });
            container.Register<ICollectionJsonDocumentReader<Friend>, FriendDocumentReader>();
            container.Register<IWriteDocument,WriteDocument>();
        }

        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);


        }
    }
}

