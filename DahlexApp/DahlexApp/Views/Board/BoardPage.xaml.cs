using MvvmCross.Forms.Views;


namespace DahlexApp.Views.Board
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

            base.ViewModel.TheProfImage = TheProf;
            base.ViewModel.TheHeapImage = TheHeap;
            base.ViewModel.TheRobotImage = TheRobot;
            base.ViewModel.TheAbsBoard = TheBoard;
        }
    }
}
