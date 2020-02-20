using System.Threading.Tasks;
using DahlexApp.Views.Start;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace DahlexApp.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<StartViewModel>();
        }
    }
}
