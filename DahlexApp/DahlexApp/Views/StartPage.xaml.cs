using DahlexApp.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;


namespace DahlexApp.Views
{
    public partial class StartPage : MvxContentPage<StartViewModel>
    {
       // ItemsViewModel viewModel;

        public StartPage()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                // handle the tap
                TheX.TranslateTo(TheX.TranslationX+10, TheX.TranslationY+10, 250U);
            };
            TheX.GestureRecognizers.Add(tapGestureRecognizer);

         
        }

    }
}