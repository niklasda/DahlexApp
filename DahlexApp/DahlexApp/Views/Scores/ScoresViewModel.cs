using System.Linq;
using System.Threading.Tasks;
using DahlexApp.Logic.HighScores;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace DahlexApp.Views.Scores
{
    public class ScoresViewModel : MvxViewModel
    {
        private readonly IGameService _gs;
        private readonly IHighScoreService _scores;

        // todo add base model with navigation etc

        public ScoresViewModel(IGameService gs, IHighScoreService scores)
        {
            _gs = gs;
            _scores = scores;

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
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MvxObservableCollection<ScoreItemViewModel> HighScoreList { get; } = new MvxObservableCollection<ScoreItemViewModel>();

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            var scores = _scores.LoadLocalHighScores();
            HighScoreList.AddRange(scores.Select(_=>new ScoreItemViewModel{Title = _.Content}));

        }
    }
}
