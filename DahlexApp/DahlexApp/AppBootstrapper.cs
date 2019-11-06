
namespace DahlexApp
{
    public class AppBootstrapper 
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