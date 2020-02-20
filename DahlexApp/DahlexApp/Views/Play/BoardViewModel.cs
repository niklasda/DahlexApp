using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.Views.Play
{
    public class BoardViewModel : MvxViewModel<string>
    {

        public BoardViewModel(IGameService gs)
        {
            _gs = gs;

            Title = "Play";

            ShortestDimension = Math.Min((int)Application.Current.MainPage.Width, (int)Application.Current.MainPage.Height);

            PlanetImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.planet_01.png"); // 42x42

            string[] resourceNames = this.GetType().Assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                Debug.WriteLine(resourceName);
            }

            ClickedTheXCommand = new MvxCommand(() =>
            {
                TheXImage.TranslateTo(TheXImage.TranslationX + 40, TheXImage.TranslationY + 40, 250U);

                //               var task = Mvx.IoCProvider.Resolve<IMvxWebBrowserTask>();
                //  _browser.ShowWebPage("http://www.xamarin.com");
            });
        }

        private readonly IGameService _gs;

        public ImageSource PlanetImageSource { get; set; }

        public override void Prepare(string what)
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            //TODO: Add starting logic here
        }

        public IMvxCommand ClickedTheXCommand { get; }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            // Debug.WriteLine("destroy");
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {

                    BoxView bv = new BoxView();
                    //bv.Margin = 0;
                    
                    if (x % 2 == 0 && y % 2 == 1 || x % 2 == 1 && y % 2 == 0)
                    {
                        bv.Color = Color.Orange;
                    }
                    else
                    {
                        bv.Color = Color.DarkOrange;

                    }

                    AbsoluteLayout.SetLayoutBounds(bv, new Rectangle(0.1 * x, 0.1 * y, 0.1, 0.1));

                    AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.All);
                    TheAbsBoard.Children.Add(bv);

                }
            }
        }


        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _shortestDimension;
        public int ShortestDimension
        {
            get { return _shortestDimension; }
            set { SetProperty(ref _shortestDimension, value); }
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Image TheXImage { get; set; }

        public AbsoluteLayout TheAbsBoard { get; set; }
    }
}
