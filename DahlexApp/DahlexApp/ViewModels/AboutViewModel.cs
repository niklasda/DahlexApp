//using System;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using MvvmCross.Commands;
//using Xamarin.Essentials;
//using Xamarin.Forms;

//namespace DahlexApp.ViewModels
//{
//    public class AboutViewModel : BaseViewModel
//    {
//        public AboutViewModel()
//        {
//            Title = "About";

//            OpenWebCommand = new MvxCommand(() => Launcher.OpenAsync(new Uri("https://xamarin.com/platform")));
//        }

//        public override Task Initialize()
//        {
//            //TODO: Add starting logic here

//            return base.Initialize();
//        }

//        public IMvxCommand OpenWebCommand { get; }
//    }
//}