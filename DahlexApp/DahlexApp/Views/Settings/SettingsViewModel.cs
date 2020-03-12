using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.WebBrowser;
using MvvmCross.ViewModels;

namespace DahlexApp.Views.Settings
{
    public class SettingsViewModel : MvxViewModel
    {
        private readonly IGameService _gs;
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxWebBrowserTask _browser;

        // todo add base model with navigation etc

        public SettingsViewModel(IGameService gs, IMvxNavigationService navigationService, IMvxWebBrowserTask browser)
        {
            _gs = gs;
            _navigationService = navigationService;
            _browser = browser;

            Title = "Dahlex";

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
        public IMvxCommand GotoBoardCommand { get; }



        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
