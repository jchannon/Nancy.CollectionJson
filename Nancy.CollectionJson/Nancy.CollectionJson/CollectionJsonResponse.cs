using System;
using System.IO;
using Nancy.Json;

namespace Nancy.CollectionJson
{
    public class CollectionJsonResponse : Response
    {
        private readonly ISerializer serializer;

        private readonly ICollectionJsonDocumentWriterFactory writerfactory;

        private readonly NancyContext context;

        public CollectionJsonResponse(object model, ISerializer serializer, ICollectionJsonDocumentWriterFactory writerfactory, NancyContext context)
        {
            this.context = context;
            if (serializer == null)
            {
                throw new InvalidOperationException("JSON Serializer not set");
            }

            this.serializer = serializer;
            this.writerfactory = writerfactory;
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
            var viewmodel = new CollectionJsonViewModelThatIsntAViewModel();
            viewmodel.Model = model;

            viewmodel.Links = writerfactory.Get(model, this.context);

            return stream => serializer.Serialize(DefaultContentType, viewmodel, stream);
        }
    }

}

