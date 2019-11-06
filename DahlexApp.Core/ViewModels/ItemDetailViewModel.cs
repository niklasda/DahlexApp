
using DahlexApp.Core.Models;
using MvvmCross.ViewModels;

namespace DahlexApp.Core.ViewModels
{
    public class ItemDetailViewModel : MvxViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
