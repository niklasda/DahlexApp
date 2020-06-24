using Xamarin.Forms;

namespace DahlexApp.Views.Board
{
    public partial class BoardPage //: MvxContentPage<BoardViewModel>
    {
        public BoardPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ViewModel.TheAbsBoard = TheBoard;
            ViewModel.TheAbsOverBoard = TheOverBoard;
        }
    }
}
