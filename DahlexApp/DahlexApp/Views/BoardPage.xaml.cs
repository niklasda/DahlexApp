using DahlexApp.ViewModels;
using MvvmCross.Forms.Views;


namespace DahlexApp.Views
{
    public partial class BoardPage : MvxContentPage<BoardViewModel>
    {
        public BoardPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            base.ViewModel.TheXImage = TheX;
            base.ViewModel.TheAbsBoard = TheBoard;
        }
    }
}
