﻿using System.Diagnostics;
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

            base.ViewModel.TheProfImage = TheProf;
            base.ViewModel.TheHeapImage = TheHeap;
            base.ViewModel.TheRobotImage = TheRobot;
            base.ViewModel.TheAbsBoard = TheBoard;
        }


        private void SwipeGestureRecognizer_OnSwiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine(e.Direction);

        }
    }


}