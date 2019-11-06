using System.ComponentModel;
using DahlexApp.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace DahlexApp.Views
{
    public partial class AboutPage : MvxContentPage<AboutViewModel>
    {
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}