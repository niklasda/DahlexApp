using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DahlexApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace DahlexApp.Core.ViewModels
{
    public class ItemsViewModel : MvxViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public IMvxCommand LoadItemsCommand { get; set; }
        private readonly IMvxNavigationService _navigationService;

        public ItemsViewModel(IMvxNavigationService navigationService)
        {
        //    IMvxMessenger messenger

            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());

          //  MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
          //  {
            //    var newItem = item as Item;
                //Items.Add(newItem);
       //         await DataStore.AddItemAsync(newItem);
          //  });
        }

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

                //ShowViewModel<AboutViewModel>();
                //    var items = await DataStore.GetItemsAsync(true);
                //   foreach (var item in items)
                //   {
                //       Items.Add(item);
                //   }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}