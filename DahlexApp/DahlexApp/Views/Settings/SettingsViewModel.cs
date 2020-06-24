using System.Threading.Tasks;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Settings;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Size = System.Drawing.Size;

namespace DahlexApp.Views.Settings
{
    public class SettingsViewModel : MvxViewModel
    {

        // todo add base model with navigation etc

        public SettingsViewModel(IMvxNavigationService navigationService)
        {
            //_navigationService = navigationService;

            BackCommand = new MvxCommand(async () => { await navigationService.Close(this); });
            CloseImage = ImageSource.FromResource("DahlexApp.Assets.Images.Close.png");


            Title = "Settings";

        }
        //private readonly IMvxNavigationService _navigationService;

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override async Task Initialize()
        {
            //TODO: Add starting logic here

            await base.Initialize();

            // do the heavy work here

            SettingsManager sm = new SettingsManager(new Size(0,0));
            var gs = sm.LoadLocalSettings();
            ProfName = gs.PlayerName;
            IsMuted = gs.LessSound;
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();

            SettingsManager sm = new SettingsManager(new Size(0, 0));
            sm.SaveLocalSettings(new GameSettings(new Size(0,0) ){PlayerName = ProfName, LessSound = IsMuted});

        }

        public IMvxCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _profName;
        public string ProfName
        {
            get => _profName;
            set => SetProperty(ref _profName, value);
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set => SetProperty(ref _isMuted, value);
        }
    }
}
