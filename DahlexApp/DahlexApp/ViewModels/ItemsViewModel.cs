//using System;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Threading.Tasks;

//using Xamarin.Forms;

//using DahlexApp.Models;
//using DahlexApp.Views;
//using MvvmCross.Commands;
//using MvvmCross.ViewModels;

//namespace DahlexApp.ViewModels
//{
//    public class ItemsViewModel : BaseViewModel
//    {
//        public ObservableCollection<Item> Items { get; set; }
//        public IMvxCommand LoadItemsCommand { get; set; }

//        public ItemsViewModel()
//        {
//            Title = "Browse";
//            Items = new ObservableCollection<Item>();
//            LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());

//            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
//            {
//                var newItem = item as Item;
//                Items.Add(newItem);
//       //         await DataStore.AddItemAsync(newItem);
//            });
//        }

//        public override Task Initialize()
//        {
//            //TODO: Add starting logic here

//            return base.Initialize();
//        }

//        async Task ExecuteLoadItemsCommand()
//        {
//            if (IsBusy)
//                return;

//            IsBusy = true;

//            try
//            {
//                Items.Clear();

//                //ShowViewModel<AboutViewModel>();
//                //    var items = await DataStore.GetItemsAsync(true);
//                //   foreach (var item in items)
//                //   {
//                //       Items.Add(item);
//                //   }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex);
//            }
//            finally
//            {
//                IsBusy = false;
//            }
//        }
//    }
//}