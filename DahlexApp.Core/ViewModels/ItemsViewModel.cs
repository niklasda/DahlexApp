using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DahlexApp.Core.Models;
using DahlexApp.Logic.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace DahlexApp.Core.ViewModels
{
    public class ItemsViewModel :  MvxViewModel<string>
    {
        private readonly IGameService _gs;
        // public ObservableCollection<Item> Items { get; set; }
        // public IMvxCommand LoadItemsCommand { get; set; }
        // private readonly IMvxNavigationService _navigationService;

        public ItemsViewModel(IGameService gs)
        {
            _gs = gs;
            //    IMvxMessenger messenger

            Title = "Browse";
            // Items = new ObservableCollection<Item>();
            //LoadItemsCommand = new MvxCommand(async () => await ExecuteLoadItemsCommand());

          //  MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
          //  {
            //    var newItem = item as Item;
                //Items.Add(newItem);
       //         await DataStore.AddItemAsync(newItem);
          //  });
        }

        public override void Prepare(string what)
        {
            // first callback. Initialize parameter-agnostic stuff here

            var asd = what;
        }

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        private string _title ;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _text1;
        public string Text1
        {
            get { return _text1; }
            set { SetProperty(ref _text1, value); }
        }

        private string _text2 ;
        public string Text2
        {
            get { return _text2; }
            set { SetProperty(ref _text2, value); }
        }


        private bool _isBusy ;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

       
    }
}