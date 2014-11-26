using CollectionJson;
using Nancy.CollectionJson.Demo.ViewModels;

namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IFriendsClient
    {
        Collection Get(int id);
        Collection List();
        IFriendClient FriendClient { get; }
    }
}