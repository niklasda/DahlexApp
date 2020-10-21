using Foundation;
using UIKit;
using DahlexApp.Common;

namespace DahlexApp.iOS.Controls
{
    public class ShowToastPopUp : IToastPopUp
    {
        private const double LongDelay = 3.5;
        private const double ShortDelay = 2.0;

        //private NSTimer _alertDelay;
        //private UIAlertController _alert;

        private void ShowToast(string message, bool showLong)
        {
            if (showLong)
            {
                ShowToastAlert(message, LongDelay);
            }
            else
            {
                ShowToastAlert(message, ShortDelay);
            }
        }

        private void ShowToastAlert(string message, double seconds)
        {
            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
            {
                DismissMessage(alert, obj);
            });

//            var tView = _alert.View;


            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

        }


        private void DismissMessage(UIAlertController alert, NSTimer alertDelay)
        {
            alert?.DismissViewController(true, null);

            alertDelay?.Dispose();
        }

        /// <summary>
        /// Show Message
        /// in a Toast
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastMessage(string message)
        {
            ShowToast(message, false);
        }

        
    }
}
