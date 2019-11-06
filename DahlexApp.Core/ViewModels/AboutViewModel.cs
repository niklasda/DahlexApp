using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DahlexApp.Logic.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.WebBrowser;
using MvvmCross.ViewModels;

namespace DahlexApp.Core.ViewModels
{
    public class AboutViewModel : MvxViewModel
    {
        private readonly IGameService _gs;

        public AboutViewModel(IGameService gs)
        {
            _gs = gs;
            // Title = "About";

            OpenWebCommand = new MvxCommand(() =>
            {
               // PluginLoader.Instance.EnsureLoaded();
                var task = Mvx.IoCProvider.Resolve<IMvxWebBrowserTask>();
                task.ShowWebPage("http://www.xamarin.com");
                //Launcher.OpenAsync(new Uri("https://xamarin.com/platform"));
            });
        }

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        public IMvxCommand OpenWebCommand { get; }

        

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}