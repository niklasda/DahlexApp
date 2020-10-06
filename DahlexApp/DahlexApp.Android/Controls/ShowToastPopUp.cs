using Android.Graphics;
using Android.Widget;
using DahlexApp.Common;

namespace DahlexApp.Droid.Controls
{
    public class ShowToastPopUp : IToastPopUp
    {
        private static Toast _toastInstance;

        private void ShowToast(string message, ToastLength duration, Color bgColor, Color txtColor)
        {
            _toastInstance?.Cancel();

            _toastInstance = Toast.MakeText(Android.App.Application.Context, message, duration);

            //View tView = _toastInstance.View;
            //tView.Background.SetColorFilter(bgColor, PorterDuff.Mode.SrcIn); // Gets the actual oval background of the Toast then sets the color filter

            //TextView text = (TextView)tView.FindViewById(Android.Resource.Id.Message);
            //text.SetTextColor(txtColor);

            _toastInstance.Show();
        }

        /// <summary>
        /// ShowToastError
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastError(string message)
        {
            ShowToast(message, ToastLength.Short, Color.ParseColor("#9f333c"), Color.White);

        }

        /// <summary>
        /// ShowToastMessage
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastMessage(string message)
        {
            ShowToast(message, ToastLength.Short, Color.ParseColor("#212636"), Color.White);

        }

        /// <summary>
        /// ShowToastSuccess
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastSuccess(string message)
        {
            ShowToast(message, ToastLength.Short, Color.ParseColor("#70B771"), Color.White);

        }

        /// <summary>
        /// ShowToastWarning
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastWarning(string message)
        {
            ShowToast(message, ToastLength.Short, Color.ParseColor("#faaa1d"), Color.White);

        }
    }
}
