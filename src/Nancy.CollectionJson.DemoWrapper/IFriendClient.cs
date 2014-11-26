using CollectionJson;

namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IFriendClient
    {
        Collection Search(string friendName);
    }
}