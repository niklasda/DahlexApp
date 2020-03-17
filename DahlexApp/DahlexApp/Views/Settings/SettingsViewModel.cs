using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace DahlexApp.Views.Settings
{
    public class SettingsViewModel : MvxViewModel
    {
        //private readonly IMvxWebBrowserTask _browser;

        // todo add base model with navigation etc

        public SettingsViewModel()
        {
          //  _browser = browser;

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



        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
