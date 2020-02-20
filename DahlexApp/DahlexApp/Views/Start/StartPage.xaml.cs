using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace DahlexApp.Views.Start
{
    public partial class StartPage : MvxContentPage<StartViewModel>
    {
        public StartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
