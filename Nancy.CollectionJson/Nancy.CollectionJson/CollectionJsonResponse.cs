using System;
using System.IO;
using Nancy.Json;
using System.Collections.Generic;

namespace Nancy.CollectionJson
{
    public class CollectionJsonResponse : Response
    {
        private readonly ISerializer serializer;

        private readonly NancyContext context;

        private readonly IEnumerable<ILinkGenerator> linkGenerators;

        public CollectionJsonResponse(object model, ISerializer serializer, IEnumerable<ILinkGenerator> linkGenerators, NancyContext context)
        {
            this.context = context;
            if (serializer == null)
            {
                throw new InvalidOperationException("JSON Serializer not set");
            }

            this.linkGenerators = linkGenerators;
            this.serializer = serializer;
            this.Contents = model == null ? NoBody : GetCollectionJsonContents(model);
            this.ContentType = "application/vnd.collection+json"; 
            this.StatusCode = HttpStatusCode.OK;

        }

        private static string DefaultContentType
        {
            get { return string.Concat("application/json", Encoding); }
        }

        private static string Encoding
        {
            get
            {
                return !string.IsNullOrWhiteSpace(JsonSettings.DefaultCharset)
                    ? string.Concat("; charset=", JsonSettings.DefaultCharset)
                        : string.Empty;
            }
        }

        private Action<Stream> GetCollectionJsonContents(object model)
        {
            object viewmodel = null;
            foreach (var item in this.linkGenerators)
            {
                if (item.CanHandle(model.GetType()))
                {
                    viewmodel = item.Handle(model, this.context);

                }
            }

            return stream => serializer.Serialize(DefaultContentType, viewmodel, stream);
        }
    }

}

