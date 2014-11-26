namespace Nancy.CollectionJson.DemoWrapper
{
    public class RootClient : IRootClient
    {
        private readonly IHomeDocProvider homeDocProvider;
        private readonly IApiConnection apiConnection;

        public RootClient(IHomeDocProvider homeDocProvider, IApiConnection apiConnection)
        {
            this.homeDocProvider = homeDocProvider;
            this.apiConnection = apiConnection;
        }

        public void Connect()
        {
            var links = this.homeDocProvider.GetLinks();

            //We might have a depedendency injection problem here??
            FriendsClient = new FriendsClient(links, this.apiConnection);
        }

        public IFriendsClient FriendsClient
        {
            get;
            private set;
        }
    }
}