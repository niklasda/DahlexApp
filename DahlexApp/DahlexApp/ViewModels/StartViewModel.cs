using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.ViewModels
{
    public class StartViewModel :  MvxViewModel<string>
    {
        private readonly IGameService _gs;

        public StartViewModel(IGameService gs)
        {
            _gs = gs;

            Title = "Browse";

            FlagImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.Xamarin120.png");

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

        }

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

        private string _title ;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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


        private string _text2 ;
        public string Text2
        {
            get { return _text2; }
            set { SetProperty(ref _text2, value); }
        }


        private bool _isBusy ;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

       
    }
}