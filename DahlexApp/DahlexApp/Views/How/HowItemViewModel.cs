using Xamarin.Forms;

namespace DahlexApp.Views.How
{
    public class HowItemViewModel 
    {
        private string _imageText;
        public string ImageText
        {
            get => _imageText;
            set => _imageText = value;
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }
    }
}
