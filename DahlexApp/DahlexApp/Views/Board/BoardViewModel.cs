using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Essentials;
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
            });

            ClickedTheHeapCommand = new MvxCommand(() =>
            {
                TheHeapImage.TranslateTo(TheHeapImage.TranslationX + 40, TheHeapImage.TranslationY + 40, 250U);
            });

            ClickedTheRobotCommand = new MvxCommand(() =>
            {
                TheRobotImage.TranslateTo(TheRobotImage.TranslationX + 40, TheRobotImage.TranslationY + 40, 250U);
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

            GoDirCommand = new MvxCommand<string>((d) =>
            {
                Application.Current.MainPage.DisplayAlert("Dahlex", $"{d}", "Ok");
            });

            BombCommand = new MvxCommand(() =>
            {
                try
                {
                    // Use default vibration length
                    Vibration.Vibrate();

                    // Or use specified time
                   // var duration = TimeSpan.FromSeconds(1);
                   // Vibration.Vibrate(duration);
                }
                catch (Exception )
                {
                    // Other error has occurred.
                }
            });

        }

        private readonly IGameService _gs;

        public ImageSource PlanetImageSource { get; set; }
        public ImageSource HeapImageSource { get; set; }
        public ImageSource Robot1ImageSource { get; set; }
        public ImageSource Robot2ImageSource { get; set; }
        public ImageSource Robot3ImageSource { get; set; }

        public IMvxCommand BombCommand { get; }
        public IMvxCommand ComingSoonCommand { get; }
        public IMvxCommand StartGameCommand { get; }
        public IMvxCommand<string> GoDirCommand { get; }

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
                    bv.GestureRecognizers.Add(new TapGestureRecognizer(){Command = BombCommand });
                    AbsoluteLayout.SetLayoutBounds(bv, new Rectangle(40 * x, 40 * y, 40, 40));
                    AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.None);
                    TheAbsBoard.Children.Add(bv);
                }
            }

            TheProfImage.IsVisible = false;
            TheHeapImage.IsVisible = false;
            TheRobotImage.IsVisible = false;

            AbsoluteLayout.SetLayoutBounds(TheProfImage, new Rectangle(40 * 2, 40 * 1, 40, 40));
            AbsoluteLayout.SetLayoutFlags(TheProfImage, AbsoluteLayoutFlags.None);

            AbsoluteLayout.SetLayoutBounds(TheHeapImage, new Rectangle(40 * 3, 40 * 2, 40, 40));
            AbsoluteLayout.SetLayoutFlags(TheHeapImage, AbsoluteLayoutFlags.None);

            AbsoluteLayout.SetLayoutBounds(TheRobotImage, new Rectangle(40 * 4, 40 * 3, 40, 40));
            AbsoluteLayout.SetLayoutFlags(TheRobotImage, AbsoluteLayoutFlags.None);

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
