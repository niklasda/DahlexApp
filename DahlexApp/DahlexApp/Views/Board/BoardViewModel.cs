using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.Views.Board
{
    public class BoardViewModel : MvxViewModel<string>
    {

        public BoardViewModel(IGameService gs)
        {
            _gs = gs;

            Title = "Play";

            ShortestDimension = Math.Min((int)Application.Current.MainPage.Width, (int)Application.Current.MainPage.Height);

            // 42x42
            PlanetImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.planet_01.png"); 
            HeapImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.heap_02.png"); 
            Robot1ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.robot_04.png"); 
            Robot2ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.robot_05.png"); 
            Robot3ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.robot_06.png"); 

            string[] resourceNames = this.GetType().Assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                Debug.WriteLine(resourceName);
            }

            ClickedTheProfCommand = new MvxCommand(() =>
            {
                TheProfImage.TranslateTo(TheProfImage.TranslationX + 40, TheProfImage.TranslationY + 40, 250U);
                TheHeapImage.TranslateTo(TheHeapImage.TranslationX + 40, TheHeapImage.TranslationY + 40, 250U);
                TheRobotImage.TranslateTo(TheRobotImage.TranslationX + 40, TheRobotImage.TranslationY + 40, 250U);

                //               var task = Mvx.IoCProvider.Resolve<IMvxWebBrowserTask>();
                //  _browser.ShowWebPage("http://www.xamarin.com");
            });

            StartGameCommand = new MvxCommand(() =>
            {
                TheProfImage.IsVisible = !TheProfImage.IsVisible;
                TheHeapImage.IsVisible = !TheHeapImage.IsVisible;
                TheRobotImage.IsVisible = !TheRobotImage.IsVisible;
            });

            ComingSoonCommand = new MvxCommand(() =>
            {
                Application.Current.MainPage.DisplayAlert("Dahlex", "Coming SoOon", "Ok");
            });
        }

        private readonly IGameService _gs;

        public ImageSource PlanetImageSource { get; set; }
        public ImageSource HeapImageSource { get; set; }
        public ImageSource Robot1ImageSource { get; set; }
        public ImageSource Robot2ImageSource { get; set; }
        public ImageSource Robot3ImageSource { get; set; }

        public IMvxCommand ComingSoonCommand { get; }
        public IMvxCommand StartGameCommand { get; }

        public override void Prepare(string what)
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            //TODO: Add starting logic here
        }

        public IMvxCommand ClickedTheProfCommand { get; }
        public IMvxCommand ClickedTheHeapCommand { get; }
        public IMvxCommand ClickedTheRobotCommand { get; }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    BoxView bv = new BoxView();
                    
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

            TheProfImage.IsVisible = false;
            TheHeapImage.IsVisible = false;
            TheRobotImage.IsVisible = false;


            //TheXImage = new Image();
            //TheXImage.Source = PlanetImageSource;
            //TheXImage.GestureRecognizers.Add(new TapGestureRecognizer() {Command = ClickedTheXCommand});
            //AbsoluteLayout.SetLayoutBounds(TheXImage, new Rectangle(0.1, 0.3, 0.1, 0.1));
            //AbsoluteLayout.SetLayoutFlags(TheXImage, AbsoluteLayoutFlags.All);
            //TheAbsBoard.Children.Add(TheXImage);

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

        public Image TheProfImage { get; set; }
        public Image TheHeapImage { get; set; }
        public Image TheRobotImage { get; set; }

        public AbsoluteLayout TheAbsBoard { get; set; }
    }
}
