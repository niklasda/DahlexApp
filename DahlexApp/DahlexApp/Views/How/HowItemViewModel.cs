using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace DahlexApp.Views.How
{
    public class HowItemViewModel 
    {


        private string _imageName;
        public string ImageName
        {
            get => _imageName;
            set => _imageName = value;
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }

    }
}
