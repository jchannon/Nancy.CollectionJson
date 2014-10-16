using CollectionJson;

namespace Nancy.CollectionJson
{
    public interface ICollectionJsonDocumentWriterFactory
    {
        Collection Get(object model, NancyContext context);
    }
}

