using Android.Widget;
using DahlexApp.Common;

namespace DahlexApp.Droid.Controls
{
    public class ShowToastPopUp : IToastPopUp
    {
        private static Toast _toastInstance;

        private void ShowToast(string message, ToastLength duration)
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
        /// ShowToastMessage
        /// </summary>
        /// <param name="message"></param>
        public void ShowToastMessage(string message)
        {
            ShowToast(message, ToastLength.Short);

        }

        
    }
}
