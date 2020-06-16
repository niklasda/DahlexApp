using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.Views.How
{
    public class HowViewModel : MvxViewModel
    {
        public HowViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            BackCommand = new MvxCommand(async () =>  {  await _navigationService.Close(this);  });
            CloseImage = ImageSource.FromResource("DahlexApp.Assets.Images.Close.png");

            Title = "How";
        }

        private readonly IMvxNavigationService _navigationService;

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

        public ObservableCollection<HowItemViewModel> HowToPages { get; } = new ObservableCollection<HowItemViewModel>();

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            HowToPages.Clear();

            HowToPages.Add(new HowItemViewModel { ImageText = "Simple", ImageSource = ImageSource.FromResource("DahlexApp.Assets.Screens.Screen1_1280.png") });
            HowToPages.Add(new HowItemViewModel { ImageText = "Who is who", ImageSource = ImageSource.FromResource("DahlexApp.Assets.Screens.Screen2_1280.png") });
            HowToPages.Add(new HowItemViewModel { ImageText = "Busy", ImageSource = ImageSource.FromResource("DahlexApp.Assets.Screens.Screen4_1280.png") });
        }

        public IMvxCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }

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
