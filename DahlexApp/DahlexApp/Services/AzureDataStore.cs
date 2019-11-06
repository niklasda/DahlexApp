//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Xamarin.Essentials;
//using DahlexApp.Models;

//namespace DahlexApp.Services
//{
//    public class AzureDataStore : IDataStore<Item>
//    {
//        private readonly HttpClient _client;
//        private IEnumerable<Item> _items;

//        public AzureDataStore()
//        {
//            _client = new HttpClient();
//            _client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

//            _items = new List<Item>();
//        }

//        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
//        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
//        {
//            if (forceRefresh && IsConnected)
//            {
//                var json = await _client.GetStringAsync($"api/item");
//                _items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
//            }

//            return _items;
//        }

//        public async Task<Item> GetItemAsync(string id)
//        {
//            if (id != null && IsConnected)
//            {
//                var json = await _client.GetStringAsync($"api/item/{id}");
//                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
//            }

//            return null;
//        }

//        public async Task<bool> AddItemAsync(Item item)
//        {
//            if (item == null || !IsConnected)
//                return false;

//            var serializedItem = JsonConvert.SerializeObject(item);

//            var response = await _client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> UpdateItemAsync(Item item)
//        {
//            if (item == null || item.Id == null || !IsConnected)
//                return false;

//            var serializedItem = JsonConvert.SerializeObject(item);
//            var buffer = Encoding.UTF8.GetBytes(serializedItem);
//            var byteContent = new ByteArrayContent(buffer);

//            var response = await _client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> DeleteItemAsync(string id)
//        {
//            if (string.IsNullOrEmpty(id) && !IsConnected)
//                return false;

//            var response = await _client.DeleteAsync($"api/item/{id}");

//            return response.IsSuccessStatusCode;
//        }
//    }
//}
