using System.ComponentModel;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DahlexApp.Views
{
    public abstract class BaseContentPage<T> : MvxContentPage<T>
        where T : MvxViewModel
    {
        protected BaseContentPage()
        {
            PropertyChanged += ContentPageBase_PropertyChanged;
        }

        private void ContentPageBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SafeAreaInsets")
            {
                if (GetIsModelX())
                {
                    if (this.Content is Grid vw)
                    {
                        var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                        //safeInsets.Top = 24;
                        vw.Margin = safeInsets;
                    }
                }
            }
        }

        public static bool GetIsModelX()
        {
            string model = DeviceInfo.Model;

            if (DeviceInfo.Platform == DevicePlatform.iOS && model.Contains("x86_64") ||
                model.Contains("iPhone10,3") ||
                model.Contains("iPhone10,6") ||
                model.Contains("iPhone11,"))
            {
                return true;
            }
            return false;
        }
    }
}
