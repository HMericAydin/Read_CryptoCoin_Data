using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinProgress
{
    public class LiveData
    {
        private readonly HttpClient _client;

        public LiveData()
        {
            _client = new HttpClient();
        }

        public async Task<float> CoinSelectionAsync(string crypto)
        {
            float price = 0;

            try
            {
                var response = await _client.GetAsync("https://api.binance.com/api/v1/ticker/price?symbol=" + crypto);
                var content = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(content);
                price = data.price;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching {crypto} price: {ex.Message}");
            }

            return price;
        }

        public async Task CryptoFollowAsync(string crypto)
        {
            float price = await CoinSelectionAsync(crypto);
            Console.WriteLine($"{crypto} = {price}");
        }
    }
}
