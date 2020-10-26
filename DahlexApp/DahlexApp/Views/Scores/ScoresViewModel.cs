using System.Linq;
using System.Threading.Tasks;
using DahlexApp.Logic.Settings;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.Views.Scores
{
    public class ScoresViewModel : MvxViewModel
    {

        public ScoresViewModel(IHighScoreService scores, IMvxNavigationService navigationService, IMvxMainThreadAsyncDispatcher dispatcher)
        {
            _scores = scores;
            _dispatcher = dispatcher;
            //_navigationService = navigationService;

            BackCommand = new MvxCommand(() => _ = Task.Run(async () => await navigationService.Close(this)));
            CloseImage = ImageSource.FromResource("DahlexApp.Assets.Images.Close.png");


        }

        private readonly IHighScoreService _scores;

        private readonly IMvxMainThreadAsyncDispatcher _dispatcher;
        // private readonly IMvxNavigationService _navigationService;

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // do the heavy work here
        }

        public IMvxCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }


        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MvxObservableCollection<ScoreItemViewModel> HighScoreList { get; } = new MvxObservableCollection<ScoreItemViewModel>();

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            _ = _dispatcher.ExecuteOnMainThreadAsync(() =>
              {
                  Title = "Scores";

                  HighScoreList.Clear();

                  var scores = _scores.LoadLocalHighScores();
                  HighScoreList.AddRange(scores.Select(_ => new ScoreItemViewModel { Title = _.Content }));
              });
        }
    }
}
