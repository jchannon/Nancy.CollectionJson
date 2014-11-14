using RestSharp;
using RestSharp.Deserializers;
using ServiceStack.Text;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class RestSharpServiceStackSerializer : IDeserializer
    {
        public T Deserialize<T>(IRestResponse response)
        {
            return response.Content.FromJson<T>();
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }
}