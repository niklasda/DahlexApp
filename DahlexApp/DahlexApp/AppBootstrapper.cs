using Splat;

namespace DahlexApp
{
    public class AppBootstrapper : IEnableLogger
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        public void RegisterDependencies()
        {
            //Locator.CurrentMutable.RegisterConstant(new FeedService(), typeof(IFeedService));
        }
    }
}