using System.Drawing;
using System.Threading.Tasks;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Settings;
using MvvmCross.ViewModels;

namespace DahlexApp.Views.Settings
{
    public class SettingsViewModel : MvxViewModel
    {

        // todo add base model with navigation etc

        public SettingsViewModel()
        {

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
