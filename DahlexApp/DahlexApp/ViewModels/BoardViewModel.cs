using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.ViewModels
{
    public class BoardViewModel : MvxViewModel<string>
    {
        private readonly IGameService _gs;

        public BoardViewModel(IGameService gs)
        {
            _gs = gs;

            Title = "Play";

            ShortestDimension = Math.Min((int)Application.Current.MainPage.Width, (int)Application.Current.MainPage.Height);

            FlagImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.Xamarin120.png");

            ClickedTheXCommand = new MvxCommand(() =>
            {
                //               var task = Mvx.IoCProvider.Resolve<IMvxWebBrowserTask>();
              //  _browser.ShowWebPage("http://www.xamarin.com");
            });
        }

        public ImageSource FlagImageSource { get; set; }

        public override void Prepare(string what)
        {

            // first callback. Initialize parameter-agnostic stuff here

            var asd = what;
        }


        public override async Task Initialize()
        {
            await base.Initialize();


            //TODO: Add starting logic here
            TextInput1 = "<write>";
            Text1 = "1";
            Text2 = "2";


            TextBacker.Subscribe(s1 => Debug.WriteLine(s1));

            // BoxView Color = "Blue" AbsoluteLayout.LayoutBounds = "0.2,0,0.1,0.1" 


          //  BoxView bv = new BoxView();
          //  bv.Color = Color.Brown;
           // TheBoard.Children.Add(bv);

          // AbsoluteLayout.SetLayoutBounds(bv, new Rectangle(0.3, 0.0, 0.1, 0.1));

          //  AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.All);


        }

        public IMvxCommand ClickedTheXCommand { get; }


        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
            // Debug.WriteLine("destroy");

        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            // Debug.WriteLine("disappear");
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

        private string _text1;
        public string Text1
        {
            get { return _text1; }
            set { SetProperty(ref _text1, value); }
        }


        private Subject<string> TextBacker = new Subject<string>();

        private string _textInput1;


        public string TextInput1
        {
            get { return _textInput1; }
            set
            {
                SetProperty(ref _textInput1, value);
                TextBacker.OnNext(value);
            }
        }


        private string _text2;
        public string Text2
        {
            get { return _text2; }
            set { SetProperty(ref _text2, value); }
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

      //  public AbsoluteLayout TheAbsBoard { get; set; }
    }
}
