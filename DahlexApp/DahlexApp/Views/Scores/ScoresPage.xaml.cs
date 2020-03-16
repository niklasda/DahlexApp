using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace DahlexApp.Views.Scores
{
    public partial class ScoresPage : MvxContentPage<ScoresViewModel>
    {
        public ScoresPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
