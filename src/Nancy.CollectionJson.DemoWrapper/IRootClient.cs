namespace Nancy.CollectionJson.DemoWrapper
{
    public interface IRootClient
    {
        void Connect();
        IFriendsClient FriendsClient { get; }
    }
}
