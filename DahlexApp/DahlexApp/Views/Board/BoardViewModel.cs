using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Dahlex.Logic.Contracts;
using Dahlex.Logic.Game;
using Dahlex.Logic.HighScores;
using Dahlex.Logic.Logger;
using Dahlex.Logic.Settings;
using DahlexApp.Logic.HighScores;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using Point = System.Drawing.Point;
using Rectangle = Xamarin.Forms.Rectangle;
using Size = System.Drawing.Size;

namespace DahlexApp.Views.Board
{
    public class BoardViewModel : MvxViewModel<string>, IDahlexView
    {

        public BoardViewModel(IGameService gs, IHighScoreService hsm)
        {
            _gs = gs;
           // var sm = new SettingsManager(new Size(420, 420));
           _settings = GetSettings();
            _ge = new GameEngine(_settings, this, hsm);

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
                bool moved = PerformRound(MoveDirection.None);
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

                _ge.StartGame(GameMode.Random);
                UpdateUI(GameStatus.GameStarted, _ge.GetState(_elapsed) );
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
                    PlaySound(Sound.Bomb);

                    Vibration.Vibrate();
                }
                catch (Exception)
                {
                }
            }, () => CanBomb);

            TeleCommand = new MvxCommand(() =>
            {
                Application.Current.MainPage.DisplayAlert("Dahlex", "teleporting", "Ok");

            }, () => CanTele);



        }

        private bool PerformRound(MoveDirection dir)
        {
            bool movedOk = false;

            if (_ge != null)
            {
                if (_ge.Status == GameStatus.LevelOngoing)
                {
                    _ge.MoveHeapsToTemp();
                    if (_ge.MoveProfessorToTemp(dir))
                    {
                        _ge.MoveRobotsToTemp();
                        _ge.CommitTemp();

                        movedOk = true;
                    }
                    else
                    {
                        AddLineToLog("P. not moved");
                    }
                }

                UpdateUI(_ge.Status, _ge.GetState(_elapsed));
            }
            return movedOk;
        }

        private GameSettings GetSettings()
        {
            ISettingsManager sm = new SettingsManager(new Size(420, 420));
            var s = sm.LoadLocalSettings();
            return s;
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

        private bool _canTele;

        public bool CanTele
        {
            get { return _canTele; }
            set
            {
                SetProperty(ref _canTele, value);
                TeleCommand.RaiseCanExecuteChanged();

            }
        }

       

        private readonly GameSettings _settings;
        private readonly IGameService _gs;
        private readonly IGameEngine _ge;

        public ImageSource PlanetImageSource { get; set; }
        public ImageSource HeapImageSource { get; set; }
        public ImageSource Robot1ImageSource { get; set; }
        public ImageSource Robot2ImageSource { get; set; }
        public ImageSource Robot3ImageSource { get; set; }

        public IMvxCommand BombCommand { get; }
        public IMvxCommand TeleCommand { get; }
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
                    bv.GestureRecognizers.Add(new TapGestureRecognizer() { Command = ClickedTheProfCommand });
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

        private string _infoText1;
        public string InfoText1
        {
            get { return _infoText1; }
            set { SetProperty(ref _infoText1, value); }
        }

        private string _infoText2;
        public string InfoText2
        {
            get { return _infoText2; }
            set { SetProperty(ref _infoText2, value); }
        }

        private string _bombText;
        public string BombText
        {
            get { return _bombText; }
            set { SetProperty(ref _bombText, value); }
        }

        private string _teleText;
        public string TeleText
        {
            get { return _teleText; }
            set { SetProperty(ref _teleText, value); }
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
        public void AddLineToLog(string log)
        {
            GameLogger.AddLineToLog(log);

        }

        public void DrawBoard(IBoard board, int xSize, int ySize)
        {
        }

        public void ShowStatus(int level, int bombCount, int teleportCount, int robotCount, int moveCount, int maxLevel)
        {
            InfoText1 = string.Format("Level: {0}/{1} ", level, maxLevel);
            InfoText2 = string.Format("Dahlex: {0}  Moves: {1}", robotCount, moveCount);
            BombText = string.Format("Bomb ({0})", bombCount);
            TeleText = string.Format("Tele ({0})", teleportCount);
        }

        public void Clear(bool all)
        {
        }

        public void PlaySound(Sound effect)
        {
            if (!_settings.LessSound)
            {

                switch (effect)
                {
                    case Sound.Bomb:
                        PlayBomb();
                        break;
                    case Sound.Teleport:
                        PlayTele();
                        break;
                    case Sound.Crash:
                        PlayCrash();
                        break;
                }
            }
        }

        private void PlayBomb()
        {
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            // var v = player.Volume;
            player.Load(GetStreamFromFile("bomb.wav"));
            player.Play();
        }

        private void PlayTele()
        {
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            // var v = player.Volume;
            player.Load(GetStreamFromFile("tele.wav"));
            player.Play();
        }

        private void PlayCrash()
        {
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            // var v = player.Volume;
            player.Load(GetStreamFromFile("heap.wav"));
            player.Play();
        }

        private Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("DahlexApp.Assets.Audio." + filename);
            return stream;
        }

        public void Animate(BoardPosition bp, Point oldPosition, Point newPosition, Guid roundId)
        {
        }

        public void RemoveImage(string imageName)
        {
        }

        public void StartTheRobots(Guid roundId)
        {
        }

        public void DrawLines()
        {
        }
    }
}
