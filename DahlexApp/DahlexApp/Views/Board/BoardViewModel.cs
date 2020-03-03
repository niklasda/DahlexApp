using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Dahlex.Logic.Contracts;
using Dahlex.Logic.Game;
using Dahlex.Logic.Settings;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using Rectangle = Xamarin.Forms.Rectangle;
using Size = System.Drawing.Size;

namespace DahlexApp.Views.Board
{
    public class BoardViewModel : MvxViewModel<string>, IDahlexView
    {

        public BoardViewModel(IGameService gs)
        {
            _gs = gs;
            var sm = new SettingsManager(new Size(420, 420));
            _ge = new GameEngine(sm.LoadLocalSettings(), this);

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
                _gameTimer?.Stop();

                _gameTimer = new Timer(1000);
                _gameTimer.Elapsed += _gameTimer_Elapsed;
                _gameTimer.Start();

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
                    PlayBomb();

                    Vibration.Vibrate();
                }
                catch (Exception)
                {
                }
            }, () => CanBomb);


           
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_ge.Status == GameStatus.LevelOngoing)
            {
                _elapsed = _elapsed.Add(new TimeSpan(0, 0, 1));
                InfoText = $"{_elapsed.ToString()}";
            }
            else
            {
                _gameTimer.Stop();
            }
        }

        private bool _canBomb;
        public bool CanBomb
        {
            get { return _canBomb; }
            set
            {
                SetProperty(ref _canBomb, value);
                BombCommand.RaiseCanExecuteChanged();
                
            }
        }

        private void PlayBomb()
        {
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            // var v = player.Volume;
            player.Load(GetStreamFromFile("bomb.wav"));
            player.Play();
        }

        private Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("DahlexApp.Assets.Audio." + filename);
            return stream;
        }

        private readonly IGameService _gs;
        private readonly IGameEngine _ge;

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

            _ge.StartGame(GameMode.Campaign);
            UpdateUI(_ge.Status, _ge.GetState(_elapsed));
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            //TODO: Add starting logic here
        }

        public IMvxCommand ClickedTheProfCommand { get; }
        public IMvxCommand ClickedTheHeapCommand { get; }
        public IMvxCommand ClickedTheRobotCommand { get; }

        private TimeSpan _elapsed = TimeSpan.Zero;

        private void UpdateUI(GameStatus gameStatus, IGameState state)
        {
            if (gameStatus == GameStatus.BeforeStart)
            {
                CanBomb = false;
                CanBomb = false;
            }
            else if (gameStatus == GameStatus.GameStarted)
            {
                //AddLineToLog("Game started");
                CanBomb = true;
                CanBomb = true;

                InfoText = state.Message;
            }
        }

        

        private Timer _gameTimer;

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
                    bv.GestureRecognizers.Add(new TapGestureRecognizer() { Command = BombCommand });
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

           // _gameTimer.Stop();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            _gameTimer.Stop();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _infoText;
        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value); }
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
