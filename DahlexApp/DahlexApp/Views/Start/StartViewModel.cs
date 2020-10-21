using System.Threading.Tasks;
using DahlexApp.Logic.Models;
using DahlexApp.Views.Board;
using DahlexApp.Views.How;
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
            //_navigationService = navigationService;

            Title = "Dahlex";

            LogoImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.Tile300.png"); // 42x42

            HowCommand = new MvxCommand(() => _ = Task.Run(async () => await navigationService.Navigate<HowViewModel>())
            //Application.Current.MainPage.DisplayAlert("Dahlex","Coming SoOon","Ok");
            );

            GotoBoardCommand = new MvxCommand(() =>

                _ = Task.Run(async () => await navigationService.Navigate<BoardViewModel, GameModeModel>(new GameModeModel { SelectedGameMode = GameMode.Random }))
            );

            GotoTutorialCommand = new MvxCommand(() =>

                _ = Task.Run(async () => await navigationService.Navigate<BoardViewModel, GameModeModel>(new GameModeModel { SelectedGameMode = GameMode.Campaign }))
            );

            GotoSettingsCommand = new MvxCommand(() =>

                _ = Task.Run(async () => await navigationService.Navigate<SettingsViewModel>())
            );

            GotoScoresCommand = new MvxCommand(() =>

                _ = Task.Run(async () => await navigationService.Navigate<ScoresViewModel>())
            );
        }

        //private readonly IMvxNavigationService _navigationService;

        public ImageSource LogoImageSource { get; set; }

        // todo add base model with navigation etc


        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // do the heavy work here
        }

        public IMvxCommand HowCommand { get; }
        public IMvxCommand GotoBoardCommand { get; }
        public IMvxCommand GotoTutorialCommand { get; }
        public IMvxCommand GotoSettingsCommand { get; }
        public IMvxCommand GotoScoresCommand { get; }



        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
