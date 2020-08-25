using System;
using System.Linq;
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

        private void ShowToast(string message, UIColor bgColor, UIColor txtColor, bool showLong)
        {
            if (showLong)
            {
                ShowToastAlert(message, LongDelay, bgColor, txtColor);
            }
            else
            {
                ShowToastAlert(message, ShortDelay, bgColor, txtColor);
            }
        }

        private void ShowToastAlert(string message, double seconds, UIColor bgColor, UIColor txtColor)
        {
            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
            {
                DismissMessage(alert, obj);
            });

//            var tView = _alert.View;

            //  if (!string.IsNullOrEmpty(backgroundHexColor))
            //  {
         //   var firstSubView = tView.Subviews?.FirstOrDefault();
           // var alertContentView = firstSubView?.Subviews?.FirstOrDefault();
          //  if (alertContentView != null)
          //  {
          //      foreach (UIView uiView in alertContentView.Subviews)
             //   {
               //     uiView.BackgroundColor = bgColor;
             //   }
            //}

            //var attributedString = new NSAttributedString(message, foregroundColor: txtColor);
            //_alert.SetValueForKey(attributedString, new NSString("attributedMessage"));
            //GetVisibleViewController().PresentViewController(_alert, true, null);

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

        }

        //private UIViewController GetVisibleViewController()
        //{
        //    try
        //    {
        //        var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        //        switch (rootController.PresentedViewController)
        //        {
        //            case null:
        //                return rootController;
        //            case UINavigationController controller:
        //                return controller.VisibleViewController;
        //            case UITabBarController barController:
        //                return barController.SelectedViewController;
        //            default:
        //                return rootController.PresentedViewController;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return UIApplication.SharedApplication.KeyWindow.RootViewController;
        //    }
        //}

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
            ShowToast(message, UIColor.Black, UIColor.White, false);
        }

        
    }
}
