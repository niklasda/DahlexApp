using System.Threading.Tasks;
using DahlexApp.Views.Board;
using DahlexApp.Views.Scores;
using DahlexApp.Views.Settings;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.Views.Start
{
    public class StartViewModel : MvxViewModel
    {
        public StartViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "Dahlex";

            LogoImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.Tile300.png"); // 42x42

            ComingSoonCommand = new MvxCommand(() =>
            {
                Application.Current.MainPage.DisplayAlert("Dahlex","Coming SoOon","Ok");
            });

            GotoBoardCommand = new MvxCommand(async () =>
            {
                await _navigationService.Navigate<BoardViewModel, string>("hello");
            });

            GotoSettingsCommand = new MvxCommand(async () =>
            {
                await _navigationService.Navigate<SettingsViewModel>();
            });

            GotoScoresCommand = new MvxCommand(async () =>
            {
                await _navigationService.Navigate<ScoresViewModel>();
            });
        }

        private readonly IMvxNavigationService _navigationService;

        public ImageSource LogoImageSource { get; set; }

        // todo add base model with navigation etc


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

        public IMvxCommand ComingSoonCommand { get; }
        public IMvxCommand GotoBoardCommand { get; }
        public IMvxCommand GotoSettingsCommand { get; }
        public IMvxCommand GotoScoresCommand { get; }



        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
