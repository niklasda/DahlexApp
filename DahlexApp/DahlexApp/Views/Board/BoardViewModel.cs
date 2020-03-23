using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Game;
using DahlexApp.Logic.HighScores;
using DahlexApp.Logic.Logger;
using DahlexApp.Logic.Settings;
using DahlexApp.Logic.Utils;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Color = Xamarin.Forms.Color;
using Point = System.Drawing.Point;
//using Rectangle = Xamarin.Forms.Rectangle;
using Size = System.Drawing.Size;

namespace DahlexApp.Views.Board
{
    public class BoardViewModel : MvxViewModel<GameMode>, IDahlexView
    {

        public BoardViewModel(IHighScoreService hsm)
        {

            // var sm = new SettingsManager(new Size(420, 420));
            _settings = GetSettings();
            _ge = new GameEngine(_settings, this, hsm);

            Title = "Play";
            // w411 h660
            //ShortestDimension = Math.Min((int)Application.Current.MainPage.Width, (int)Application.Current.MainPage.Height);

            // 42x42
            // PlanetImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.planet_01.png");
            //HeapImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.heap_02.png");
            //  Robot1ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.robot_04.png");
            //  Robot2ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.robot_05.png");
            //  Robot3ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.robot_06.png");

            //string[] resourceNames = this.GetType().Assembly.GetManifestResourceNames();
            //foreach (string resourceName in resourceNames)
            // {
            //     Debug.WriteLine(resourceName);
            // }

            ClickedTheProfCommand = new MvxCommand<Point>((p) =>
            {
                PerformRound(MoveDirection.None);
            });

            // ClickedTheHeapCommand = new MvxCommand(() =>
            //  {
            //TheHeapImage.TranslateTo(TheHeapImage.TranslationX + 40, TheHeapImage.TranslationY + 40);
            //});

            //   ClickedTheRobotCommand = new MvxCommand(() =>
            // {
            //TheRobotImage.TranslateTo(TheRobotImage.TranslationX + 37, TheRobotImage.TranslationY + 37);
            //});

            StartGameCommand = new MvxCommand(() =>
            {
                _gameTimer?.Stop();
                _elapsed = TimeSpan.Zero;

                _gameTimer = new Timer(1000);
                _gameTimer.Elapsed += gameTimer_Elapsed;
                _gameTimer.Start();

                //TheProfImage.IsVisible = !TheProfImage.IsVisible;
                //TheHeapImage.IsVisible = !TheHeapImage.IsVisible;
                //TheRobotImage.IsVisible = !TheRobotImage.IsVisible;

                _ge.StartGame(GameMode.Random);
                UpdateUI(GameStatus.GameStarted, _ge.GetState(_elapsed));
            });

            ComingSoonCommand = new MvxCommand(() =>
            {
                Application.Current.MainPage.DisplayAlert("Dahlex", "Coming SoOon", "Ok");
            });

            //GoDirCommand = new MvxCommand<string>((d) =>
            //{
            //    Application.Current.MainPage.DisplayAlert("Dahlex", $"{d}", "Ok");
            //});

            NextLevelCommand = new MvxCommand(() =>
            {
                if (_ge != null)
                {
                    if (_ge.Status == GameStatus.LevelComplete)
                    {
                        //storyPanel.Resources.Clear();

                        _ge.StartNextLevel();

                        _gameTimer?.Start();
                    }

                    UpdateUI(_ge.Status, _ge.GetState(_elapsed));
                }
            });

            BombCommand = new MvxCommand(async () =>
            {
                try
                {
                    await BlowBomb();

                }
                catch (Exception)
                {
                }
            });

            TeleCommand = new MvxCommand(() =>
            {
                DoTeleport();
                //           Application.Current.MainPage.DisplayAlert("Dahlex", "teleporting", "Ok");

            });



        }


        private void DoTeleport()
        {
            if (_ge != null)
            {
                if (_ge.Status == GameStatus.LevelOngoing)
                {
                    _ge.MoveHeapsToTemp();
                    if (_ge.DoTeleport())
                    {
                        PlaySound(Sound.Teleport);

                        _ge.MoveRobotsToTemp();
                        _ge.CommitTemp();
                    }
                    else
                    {
                        AddLineToLog("No more teleports");
                    }
                }

                UpdateUI(_ge.Status, _ge.GetState(_elapsed));
            }
        }

        private async Task BlowBomb()
        {
            if (_ge != null)
            {
                if (_ge.Status == GameStatus.LevelOngoing)
                {
                    _ge.MoveHeapsToTemp();
                    if (_ge.BlowBomb())
                    {
                        try
                        {
                            await DrawExplosionRadius(_ge.GetProfessorCoordinates());
                        }
                        catch (Exception ex)
                        {
                            // safety try, marketplace version crashes on samsung 
                            //MessageBox.Show(ex.Message);
                        }

                        Vibration.Vibrate();

                        PlaySound(Sound.Bomb);
                        if (_ge.MoveProfessorToTemp(MoveDirection.None))
                        {
                            _ge.MoveRobotsToTemp();
                            _ge.CommitTemp();
                        }
                    }
                    else
                    {
                        AddLineToLog("Cannot bomb");
                    }
                }

                UpdateUI(_ge.Status, _ge.GetState(_elapsed));
            }
        }

        private async Task DrawExplosionRadius(Point pos)
        {
            Debug.WriteLine(pos.X);


            var bv = new Frame()
            {
                BorderColor = new Color(0x17, 0x1B, 0x26),
                CornerRadius = 0,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0)
            };

            AbsoluteLayout.SetLayoutBounds(bv, new Rectangle(37 * pos.X + 16, 37 * pos.Y + 16, 5, 5));
            AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.None);

            TheAbsBoard.Children.Add(bv);

            await bv.ScaleTo(15, 1000);
            await bv.ScaleTo(0.1, 1000);
            TheAbsBoard.Children.Remove(bv);
        }

        //private bool DeterminePerformRound(Point p)
        //{
        //    var pp = _ge.GetProfessorCoordinates();

        //    if (p.X > pp.X && p.Y > pp.Y)
        //    {
        //        return PerformRound(MoveDirection.SouthEast);
        //    }
        //    else if (p.X > pp.X && p.Y < pp.Y)
        //    {
        //        return PerformRound(MoveDirection.NorthEast);
        //    }
        //    else if (p.X < pp.X && p.Y > pp.Y)
        //    {
        //        return PerformRound(MoveDirection.SouthWest);
        //    }
        //    else if (p.X < pp.X && p.Y < pp.Y)
        //    {
        //        return PerformRound(MoveDirection.NorthWest);
        //    }

        //    else if (p.X > pp.X && p.Y == pp.Y)
        //    {
        //        return PerformRound(MoveDirection.East);
        //    }
        //    else if (p.X < pp.X && p.Y == pp.Y)
        //    {
        //        return PerformRound(MoveDirection.West);
        //    }
        //    else if (p.X == pp.X && p.Y > pp.Y)
        //    {
        //        return PerformRound(MoveDirection.South);
        //    }
        //    else if (p.X == pp.X && p.Y < pp.Y)
        //    {
        //        return PerformRound(MoveDirection.North);
        //    }


        //    return PerformRound(MoveDirection.None);
        //}

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
            //w411 h660    iw37 37*11=407   37*13=481
            ShortestDimension = Math.Min((int)Application.Current.MainPage.Width, (int)Application.Current.MainPage.Height);
            HeightDimension = ShortestDimension + 37 * 2;

            ISettingsManager sm = new SettingsManager(new Size(37 * 11, 37 * 13)); // w11 h13
            var s = sm.LoadLocalSettings();
            return s;
        }

        private void gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_ge.Status == GameStatus.LevelOngoing)
            {
                _elapsed = _elapsed.Add(new TimeSpan(0, 0, 1));
                TimerText = $"{_elapsed.TotalSeconds}s";
            }
            else
            {
                _gameTimer.Stop();
            }
        }

        private bool _canBomb;
        public bool CanBomb
        {
            get => _canBomb;
            set => SetProperty(ref _canBomb, value);
        }

        private bool _canTele;
        public bool CanTele
        {
            get => _canTele;
            set => SetProperty(ref _canTele, value);
        }

        private bool _canNext;
        public bool CanNext
        {
            get => _canNext;
            set => SetProperty(ref _canNext, value);
        }


        private readonly GameSettings _settings;
        // private readonly IGameService _gs;
        private readonly IGameEngine _ge;
        private GameMode _startMode;

        //public ImageSource PlanetImageSource { get; set; }
        // public ImageSource HeapImageSource { get; set; }
        // public ImageSource Robot1ImageSource { get; set; }
        //public ImageSource Robot2ImageSource { get; set; }
        //public ImageSource Robot3ImageSource { get; set; }

        public IMvxCommand BombCommand { get; }
        public IMvxCommand TeleCommand { get; }
        public IMvxCommand ComingSoonCommand { get; }
        public IMvxCommand NextLevelCommand { get; }
        public IMvxCommand StartGameCommand { get; }
        // public IMvxCommand<string> GoDirCommand { get; }

        public override void Prepare(GameMode startMode)
        {
            // first callback. Initialize parameter-agnostic stuff here

            _startMode = startMode;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            //TODO: Add starting logic here

        }

        public IMvxCommand<Point> ClickedTheProfCommand { get; }
        //   public IMvxCommand ClickedTheHeapCommand { get; }
        // public IMvxCommand ClickedTheRobotCommand { get; }

        private TimeSpan _elapsed = TimeSpan.Zero;

        private void UpdateUI(GameStatus gameStatus, IGameState state)
        {
            if (gameStatus == GameStatus.BeforeStart)
            {
                CanBomb = false;
                CanTele = false;
                CanNext = false;
            }
            else if (gameStatus == GameStatus.GameStarted)
            {
                //AddLineToLog("Game started");
                CanBomb = true;
                CanTele = true;
                CanNext = false;

                AddLineToLog("Game started");
            }
            else if (gameStatus == GameStatus.LevelComplete)
            {
                AddLineToLog("Level won");
                CanBomb = false;
                CanTele = false;
                CanNext = true;

                //InfoText = state.Message;
            }
            else if (gameStatus == GameStatus.LevelOngoing)
            {
                if (state.BombCount > 0)
                {
                    CanBomb = true;
                }
                if (state.TeleportCount > 0)
                {
                    CanTele = true;
                }
                CanNext = false;

                if (string.IsNullOrWhiteSpace(state.Message))
                {
                    AddLineToLog("Game started");
                }
                else
                {
                    InfoText = state.Message;
                }

                //InfoText = state.Message;
            }
            else if (gameStatus == GameStatus.GameLost)
            {
                AddLineToLog("You lost");
                CanBomb = false;
                CanTele = false;
                CanNext = false;
            }
            else if (gameStatus == GameStatus.GameWon)
            {
                // never happens
                //               AddLineToLog("You won");
                //             btnBomb.IsEnabled = false;
                //           btnTeleport.IsEnabled = false;
                //         _dg.AddHighScore();
            }

            if (state.BombCount < 1)
            {
                CanBomb = false;
            }

            if (state.TeleportCount < 1)
            {
                CanTele = false;
            }

            //     BoardMessage(gameStatus);
        }



        private Timer _gameTimer;

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            _ge.StartGame(_startMode);

            UpdateUI(_ge.Status, _ge.GetState(_elapsed));

            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 13; y++)
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

                    //   var tp = new TapGestureRecognizer(){Command = ClickedTheProfCommand, CommandParameter = new Point(x, y)};
                    // bv.GestureRecognizers.Add(tp);

                    AbsoluteLayout.SetLayoutBounds(bv, new Rectangle(37 * x, 37 * y, 37, 37));
                    AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.None);
                    TheAbsBoard.Children.Add(bv);
                }
            }

            var pan = new PanGestureRecognizer();
            pan.PanUpdated += Pan_PanUpdated;

            var tap = new TapGestureRecognizer();
            tap.Command = ClickedTheProfCommand;

            TheAbsOverBoard.GestureRecognizers.Clear();
            TheAbsOverBoard.GestureRecognizers.Add(pan);
            TheAbsOverBoard.GestureRecognizers.Add(tap);

            //TheProfImage.IsVisible = false;
            //TheHeapImage.IsVisible = false;
            //TheRobotImage.IsVisible = false;

            //AbsoluteLayout.SetLayoutBounds(TheProfImage, new Rectangle(37 * 2, 37 * 1, 40, 40));
            //AbsoluteLayout.SetLayoutFlags(TheProfImage, AbsoluteLayoutFlags.None);

            //AbsoluteLayout.SetLayoutBounds(TheHeapImage, new Rectangle(37 * 3, 37 * 2, 40, 40));
            //AbsoluteLayout.SetLayoutFlags(TheHeapImage, AbsoluteLayoutFlags.None);

            //AbsoluteLayout.SetLayoutBounds(TheRobotImage, new Rectangle(37 * 4, 37 * 3, 40, 40));
            //AbsoluteLayout.SetLayoutFlags(TheRobotImage, AbsoluteLayoutFlags.None);

            //TheXImage = new Image();
            //TheXImage.Source = PlanetImageSource;
            //TheXImage.GestureRecognizers.Add(new TapGestureRecognizer() {Command = ClickedTheXCommand});
            //AbsoluteLayout.SetLayoutBounds(TheXImage, new Rectangle(0.1, 0.3, 0.1, 0.1));
            //AbsoluteLayout.SetLayoutFlags(TheXImage, AbsoluteLayoutFlags.All);
            //TheAbsBoard.Children.Add(TheXImage);

        }

        private int _tempX;
        private int _tempY;

        private void Pan_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started)
            {
                _tempX = 0;
                _tempY = 0;
            }
            else if (e.StatusType == GestureStatus.Running)
            {
                _tempX = (int)e.TotalX;
                _tempY = (int)e.TotalY;
            }
            else if (e.StatusType == GestureStatus.Canceled)
            {
                //_tempX = e.TotalX;
                //_tempY = e.TotalY;
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                bool moved;
                var p = new Point(_tempX, _tempY);

                if (_ge != null && _ge.Status == GameStatus.LevelOngoing)
                {
                    //                    Point p = new Point(e.TotalManipulation.Translation.X, e.TotalManipulation.Translation.Y);
                    // very small swipe or tap is like clicking the professor in standard move mode
                    if (IsTap(p) /*&& IsWithinBounds(e)*/)
                    {
                        moved = PerformRound(MoveDirection.None);
                    }
                    else //if (IsWithinBounds(e))
                    {
                        var direction = Trig.GetSwipeDirection(p);
                        if (direction != MoveDirection.Ignore)
                        {
                            moved = PerformRound(direction);
                        }
                    }
                }



            }


        }


        private static bool IsTap(Point p)
        {
            return Trig.IsTooSmallSwipe(p);
        }

        /*
        // ugly, just for now to make it work
        private bool IsWithinBounds(ManipulationCompletedEventArgs e)
        {
            int height = (_settings.SquareSize.Height + _settings.LineWidth.X) * _settings.BoardSize.Height + 3;
            int width = (_settings.SquareSize.Width + _settings.LineWidth.X) * _settings.BoardSize.Width + 3;

            var fwe = e.ManipulationContainer as FrameworkElement;
            if (fwe != null)
            {
                //string cName = e.ManipulationContainer.GetType().Name;
                //bool validControl = cName.Equals("Grid");
                //bool validControl = fwe.Name.Equals(DahlexContent.Name) || fwe.Name.Equals("imgProfessor");
                Point p = new Point(e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
                //Debug.WriteLine(cName + " - " + fwe.Name + " " + p.ToString());

                if (fwe.Name.Equals(DahlexContent.Name))
                {
                    return 0 < p.Y
                           && p.Y <= height
                           && 0 < p.X
                           && p.X <= width;
                }

                if (fwe.Name.Equals("imgProfessor") || fwe.Name.StartsWith("imgHeap") || fwe.Name.StartsWith("imgRobot"))
                {
                    var trans = fwe.TransformToVisual(DahlexContent);
                    Point p2 = trans.Transform(p);

                    return 0 < p2.Y
                           && p2.Y <= height
                           && 0 < p2.X
                           && p2.X <= width;
                }
            }
            return false;
        }
        */

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);

            // _gameTimer.Stop();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            _gameTimer?.Stop();
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _infoText;
        public string InfoText
        {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }

        private string _timerText;
        public string TimerText
        {
            get => _timerText;
            set => SetProperty(ref _timerText, value);
        }

        private string _infoText1;
        public string InfoText1
        {
            get => _infoText1;
            set => SetProperty(ref _infoText1, value);
        }

        private string _infoText2;
        public string InfoText2
        {
            get => _infoText2;
            set => SetProperty(ref _infoText2, value);
        }

        private string _bombText;
        public string BombText
        {
            get => _bombText;
            set => SetProperty(ref _bombText, value);
        }

        private string _teleText;
        public string TeleText
        {
            get => _teleText;
            set => SetProperty(ref _teleText, value);
        }

        private int _shortestDimension;
        public int ShortestDimension
        {
            get => _shortestDimension;
            set => SetProperty(ref _shortestDimension, value);
        }

        private int _heightDimension;
        public int HeightDimension
        {
            get => _heightDimension;
            set => SetProperty(ref _heightDimension, value);
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        //public Image TheProfImage { get; set; }
        //public Image TheHeapImage { get; set; }
        //public Image TheRobotImage { get; set; }

        public AbsoluteLayout TheAbsBoard { get; set; }
        public AbsoluteLayout TheAbsOverBoard { get; set; }
        public void AddLineToLog(string log)
        {
            GameLogger.AddLineToLog(log);
            InfoText = log;

        }

        public void DrawBoard(IBoard board, int xSize, int ySize)
        {

            int xOffset = _settings.ImageOffset.X;
            int yOffset = _settings.ImageOffset.Y;
            int gridPenWidth = _settings.LineWidth.X;

            for (int x = 0; x < board.GetPositionWidth(); x++)
            {
                for (int y = 0; y < board.GetPositionHeight(); y++)
                {
                    BoardPosition cp = board.GetPosition(x, y);
                    if (cp != null)
                    {
                        int oLeft = x * (xSize + gridPenWidth) + xOffset;
                        int oTop = y * (ySize + gridPenWidth) + yOffset;

                        var pt = new Point(oLeft, oTop);

                        string imgName;
                        if (cp.Type == PieceType.Heap)
                        {
                            Image boardImage = new Image { InputTransparent = true };

                            AbsoluteLayout.SetLayoutBounds(boardImage, new Rectangle(0 * x, 0 * y, 40, 40));
                            AbsoluteLayout.SetLayoutFlags(boardImage, AbsoluteLayoutFlags.None);

                            imgName = cp.ImageName;
                            // Robot2ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.heap_02.png");
                            boardImage.AutomationId = imgName;
                            boardImage.Source = ImageSource.FromResource("DahlexApp.Assets.Images.heap_02.png");
                            TheAbsOverBoard.Children.Add(boardImage);

                            Animate(cp, new Point(0, 0), new Point(x, y), Guid.Empty, 250);

                            // boardImage = pic;
                            // Image img = AddImage(imgName, boardImage, pt, cp);
                            if (cp.IsNew)
                            {
                                //   AddToFade(img, 0, 1);
                                cp.IsNew = false;
                            }
                        }
                        else if (cp.Type == PieceType.Professor)
                        {
                            Image boardImage = new Image { InputTransparent = true };

                            AbsoluteLayout.SetLayoutBounds(boardImage, new Rectangle(0 * x, 0 * y, 40, 40));
                            AbsoluteLayout.SetLayoutFlags(boardImage, AbsoluteLayoutFlags.None);

                            imgName = cp.ImageName;
                            // boardImage.Source = LoadImage("planet_01.png");

                            //boardImage.SetValue(BindablePropertyKey.FrameworkElement.NameProperty, imgName);
                            boardImage.AutomationId = imgName;
                            boardImage.Source = ImageSource.FromResource("DahlexApp.Assets.Images.planet_01.png");
                            TheAbsOverBoard.Children.Add(boardImage);
                            //boardImage = pic;
                            //AddImage(imgName, boardImage, pt, cp);
                            Animate(cp, new Point(0, 0), new Point(x, y), Guid.Empty, 250);

                        }
                        else if (cp.Type == PieceType.Robot)
                        {
                            Image boardImage = new Image { InputTransparent = true };

                            AbsoluteLayout.SetLayoutBounds(boardImage, new Rectangle(0 * x, 0 * y, 40, 40));
                            AbsoluteLayout.SetLayoutFlags(boardImage, AbsoluteLayoutFlags.None);

                            imgName = cp.ImageName;
                            string name = Randomizer.GetRandomFromSet("DahlexApp.Assets.Images.robot_04.png", "DahlexApp.Assets.Images.robot_05.png", "DahlexApp.Assets.Images.robot_06.png");
                            boardImage.Source = ImageSource.FromResource(name);
                            boardImage.AutomationId = imgName;
                            //                         boardImage.Source = LoadImage(name);
                            TheAbsOverBoard.Children.Add(boardImage);

                            Animate(cp, new Point(0, 0), new Point(x, y), Guid.Empty, 250);

                            //     pic.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            //   boardImage = pic;
                            // AddImage(imgName, boardImage, pt, cp);
                        }
                        //else if (cp.Type == PieceType.None)
                        //{
                        // imgName = cp.ImageName;
                        // RemoveImage(imgName);
                        //}

                        //if (boardImage != null)
                        //{
                        //}
                        //else if (cp.Type != PieceType.None)
                        //{
                        //    throw new Exception("Invalid Type of BoardPosition");
                        //}
                    }
                }
            }

        }

        public void ShowStatus(int level, int bombCount, int teleportCount, int robotCount, int moveCount, int maxLevel)
        {
            InfoText1 = $"Level: {level}/{maxLevel} ";
            InfoText2 = $"Dahlex: {robotCount}  Moves: {moveCount}";
            BombText = $"Bomb ({bombCount})";
            TeleText = $"Tele ({teleportCount})";
        }

        public void Clear(bool all)
        {
            TheAbsOverBoard.Children.Clear();

        }

        public void PlaySound(Sound sound)
        {
            if (!_settings.LessSound)
            {

                switch (sound)
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

        public void Animate(BoardPosition bp, Point oldPos, Point newPos, Guid roundId, uint millis)
        {
            //  int xOffset = _settings.ImageOffset.X;
            //  int yOffset = _settings.ImageOffset.Y;
            //  int gridPenWidth = _settings.LineWidth.X;

            //int oLeft = oldPos.X * (_settings.SquareSize.Width);
            //int oTop = oldPos.Y * (_settings.SquareSize.Height);

            int nLeft = newPos.X * (_settings.SquareSize.Width);
            int nTop = newPos.Y * (_settings.SquareSize.Height);

            if (bp.Type == PieceType.Professor)
            {

                var img = TheAbsOverBoard.Children.FirstOrDefault(z => z.AutomationId == bp.ImageName);
                //               img.TranslateTo(nLeft, nTop);
                img.TranslateTo(nLeft, nTop, millis);

            }
            else if (bp.Type == PieceType.Robot)
            {
                //TheRobotImage.TranslateTo(nLeft, nTop);
                var img = TheAbsOverBoard.Children.FirstOrDefault(z => z.AutomationId == bp.ImageName);
                img.TranslateTo(nLeft, nTop, millis);

            }
            else if (bp.Type == PieceType.Heap)
            {
                //TheRobotImage.TranslateTo(nLeft, nTop);
                var img = TheAbsOverBoard.Children.FirstOrDefault(z => z.AutomationId == bp.ImageName);
                img?.TranslateTo(nLeft, nTop, millis);

            }

        }

        public void RemoveImage(string imageName)
        {
            var img = TheAbsOverBoard.Children.FirstOrDefault(z => z.AutomationId == imageName);
            TheAbsOverBoard.Children.Remove(img);
        }

        public void StartTheRobots(Guid roundId)
        {
        }

        //public void DrawLines()
        //{
        //}
    }
}
