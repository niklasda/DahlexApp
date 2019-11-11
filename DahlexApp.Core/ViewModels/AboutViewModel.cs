using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.WebBrowser;
using MvvmCross.ViewModels;

namespace DahlexApp.Core.ViewModels
{
    public class AboutViewModel : MvxViewModel
    {
        private readonly IGameService _gs;
        private readonly IMvxNavigationService _navigationService;

        // todo add base model with navigation etc

        public AboutViewModel(IGameService gs, IMvxNavigationService navigationService)
        {
            _gs = gs;
            _navigationService = navigationService;
            // Title = "About";

            OpenWebCommand = new MvxCommand(() =>
            {
                var task = Mvx.IoCProvider.Resolve<IMvxWebBrowserTask>();
                task.ShowWebPage("http://www.xamarin.com");
            });

            GotoItemsCommand = new MvxCommand(async () =>
            {
                var t = await _gs.GetTest();

                await _navigationService.Navigate<StartViewModel, string>("hello");

            });
        }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override async Task Initialize()
        {
            //TODO: Add starting logic here

            await base.Initialize();

            // do the heavy work here
        }

        public IMvxCommand OpenWebCommand { get; }
        public IMvxCommand GotoItemsCommand { get; }



        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}