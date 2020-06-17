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

        private NSTimer _alertDelay;
        private UIAlertController _alert;

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
            _alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
            {
                DismissMessage();
            });

            _alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            var tView = _alert.View;

            //  if (!string.IsNullOrEmpty(backgroundHexColor))
            //  {
            var firstSubView = tView.Subviews?.FirstOrDefault();
            var alertContentView = firstSubView?.Subviews?.FirstOrDefault();
            if (alertContentView != null)
            {
                foreach (UIView uiView in alertContentView.Subviews)
                {
                    uiView.BackgroundColor = bgColor;
                }
            }

            var attributedString = new NSAttributedString(message, foregroundColor: txtColor);
            _alert.SetValueForKey(attributedString, new NSString("attributedMessage"));
            GetVisibleViewController().PresentViewController(_alert, true, null);

        }

        private UIViewController GetVisibleViewController()
        {
            try
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case null:
                        return rootController;
                    case UINavigationController controller:
                        return controller.VisibleViewController;
                    case UITabBarController barController:
                        return barController.SelectedViewController;
                    default:
                        return rootController.PresentedViewController;
                }
            }
            catch (Exception)
            {
                return UIApplication.SharedApplication.KeyWindow.RootViewController;
            }
        }

        private void DismissMessage()
        {
            _alert?.DismissViewController(true, null);

            _alertDelay?.Dispose();
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

        /// <summary>
        /// ShowToastError
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastError(string message)
        {
            ShowToast(message, UIColor.FromRGB(159, 51, 60), UIColor.White, true);
        }

        /// <summary>
        /// ShowToastWarning
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastWarning(string message)
        {
            ShowToast(message, UIColor.FromRGB(250, 170, 29), UIColor.White, true);
        }

        /// <summary>
        /// ShowToastSuccess
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastSuccess(string message)
        {
            ShowToast(message, UIColor.FromRGB(112, 183, 113), UIColor.White, false);
        }
    }
}
