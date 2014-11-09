using System.Collections.Generic;
using CollectionJson;
using Nancy.Bootstrapper;
using Nancy.CollectionJson.Demo.Infrastructure;
using Nancy.CollectionJson.Demo.Models;
using Nancy.Responses.Negotiation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


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

            container.Register<ICollectionJsonDocumentReader<Friend>, FriendsDocumentReader>();

            container.Register<CollectionJsonDocumentWriter<Friend>, FriendsDocumentWriter>();

            container.RegisterMultiple<ILinkGenerator>(new[]{ typeof(FriendsLinkGenerator) });

            container.Register<JsonSerializer, CustomJsonSerializer>();
        }


        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(x=>x.ResponseProcessors.Remove(typeof(ViewProcessor))); }
        }


        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

        }
    }

    public sealed class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();
            this.Formatting = Formatting.Indented;
            this.NullValueHandling = NullValueHandling.Ignore;
        }
    }

}

