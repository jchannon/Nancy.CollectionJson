using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Responses.Negotiation;

namespace Nancy.CollectionJson
{
    public class CollectionJsonResponseProcessor : IResponseProcessor
    {
        private static readonly IEnumerable<Tuple<string, MediaRange>> extensionMappings =
            new[] { new Tuple<string, MediaRange>("json", new MediaRange("application/vnd.collection+json")) };

        private readonly ISerializer serializer;

        private readonly IEnumerable<ILinkGenerator> linkGenerators;

        public CollectionJsonResponseProcessor(IEnumerable<ISerializer> serializers, IEnumerable<ILinkGenerator> linkGenerators)
        {
            this.linkGenerators = linkGenerators;
            this.serializer = serializers.FirstOrDefault(x => x.CanSerialize("application/json"));
        }

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            if (IsExactCollectionJsonContentType(requestedMediaRange))
            {
                return new ProcessorMatch
                {
                    ModelResult = MatchResult.DontCare,
                    RequestedContentTypeResult = MatchResult.ExactMatch
                };
            }

            if (IsWildcardCollectionJsonContentType(requestedMediaRange))
            {
                return new ProcessorMatch
                {
                    ModelResult = MatchResult.DontCare,
                    RequestedContentTypeResult = MatchResult.NonExactMatch
                };
            }

            return new ProcessorMatch
            {
                ModelResult = MatchResult.DontCare,
                RequestedContentTypeResult = MatchResult.NoMatch
            };
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return new CollectionJsonResponse(model, this.serializer, this.linkGenerators, context);
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get
            {
                return extensionMappings;
            }
        }

        private static bool IsExactCollectionJsonContentType(MediaRange requestedContentType)
        {
            if (requestedContentType.Type.IsWildcard && requestedContentType.Subtype.IsWildcard)
            {
                return true;
            }

            return requestedContentType.Matches("application/vnd.collection+json");
        }

        private static bool IsWildcardCollectionJsonContentType(MediaRange requestedContentType)
        {
            if (!requestedContentType.Type.IsWildcard && !string.Equals("application", requestedContentType.Type, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (requestedContentType.Subtype.IsWildcard)
            {
                return true;
            }

            var subtypeString = requestedContentType.Subtype.ToString();

            return (subtypeString.StartsWith("vnd", StringComparison.InvariantCultureIgnoreCase) &&
            subtypeString.EndsWith("collection+json", StringComparison.InvariantCultureIgnoreCase));
        }
    }

}

