using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace DahlexApp.Views.Settings
{
    public partial class SettingsPage : MvxContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
