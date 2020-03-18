using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace DahlexApp.Views.How
{
    public class HowViewModel : MvxViewModel
    {
        //private readonly IMvxWebBrowserTask _browser;

        // todo add base model with navigation etc

        public HowViewModel()
        {
          //  _browser = browser;

            Title = "How";

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

        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set => SetProperty(ref _isMuted, value);
        }
    }
}
