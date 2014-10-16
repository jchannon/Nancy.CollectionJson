using System;
using System.IO;
using System.Collections.Generic;
using Nancy.Responses;
using Nancy.Json;
using System.Dynamic;

namespace Nancy.CollectionJson
{
    public class CollectionJsonResponse : Response
    {
        private readonly ISerializer serializer;

        public CollectionJsonResponse(object model, ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new InvalidOperationException("JSON Serializer not set");
            }

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
            var viewmodel = new CollectionJsonViewModelThatIsntAViewModel();
            viewmodel.Model = model;
            viewmodel.Links.Add(new Link{ Href = "http://google.com", Rel = "search" });

            return stream => serializer.Serialize(DefaultContentType, viewmodel, stream);
        }
    }

    public class CollectionJsonViewModelThatIsntAViewModel
    {
        public dynamic Model { get; set; }

        public List<Link> Links { get; set; }

        public CollectionJsonViewModelThatIsntAViewModel()
        {
            Model = new ExpandoObject();
            Links = new List<Link>();
        }

       
    }
}

