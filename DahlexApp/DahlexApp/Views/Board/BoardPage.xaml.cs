using MvvmCross.Forms.Views;
using Xamarin.Forms;


namespace DahlexApp.Views.Board
{
    public partial class BoardPage : MvxContentPage<BoardViewModel>
    {
        public BoardPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            //ViewModel.TheProfImage = TheProf;
            //ViewModel.TheHeapImage = TheHeap;
            //ViewModel.TheRobotImage = TheRobot;
            ViewModel.TheAbsBoard = TheBoard;
            ViewModel.TheAbsOverBoard = TheOverBoard;
        }


    }


}
