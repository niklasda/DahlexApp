using DahlexApp.Common;
using DahlexApp.iOS.Controls;
using Foundation;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;
using Xamarin.Forms;

namespace DahlexApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<Core.App, DahlexApp.App>, Core.App, DahlexApp.App>
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            //Forms.SetFlags("IndicatorView_Experimental");
            Forms.Init();


            LoadApplication(new App());

           // 

             bool ok = base.FinishedLaunching(app, options);

             Mvx.IoCProvider.RegisterType<IToastPopUp, ShowToastPopUp>();

             return ok;
        }
    }
}
