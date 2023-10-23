using CoinProgress;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        string[] CryptoCurrencies = new string[] { "BTCUSDT", "ETHUSDT", "XRPUSDT", "LTCUSDT", "BCHUSDT", "ADAUSDT", "DOTUSDT", "LINKUSDT", "XLMUSDT", "EOSUSDT", "XMRUSDT", "XTZUSDT", "TRXUSDT", "BNBUSDT", "VETUSDT", "NEOUSDT", "DASHUSDT", "ZECUSDT", "IOTAUSDT", "ATOMUSDT", "DOGEUSDT", "ALGOUSDT", "FILUSDT", "XEMUSDT", "WAVESUSDT", "UNIUSDT", "MKRUSDT", "ENJUSDT", "SUSHIUSDT", "SNXUSDT", "YFIUSDT", "COMPUSDT" };
        LiveData coin = new LiveData();
        /*Console.WriteLine("Enter cryptocurrency name: ");
        string crypto = Console.ReadLine();
        Console.WriteLine("The initial value is = " + await coin.CoinSelectionAsync(crypto));*/

        List<Task> tasks = new List<Task>();

        foreach (string currency in CryptoCurrencies)
        {
            tasks.Add(UpdateCryptoPrice(coin, currency));
        }

        await Task.WhenAll(tasks);
    }

    static async Task UpdateCryptoPrice(LiveData coin, string currency)
    {
        while (true)
        {
            await coin.CryptoFollowAsync(currency);
            await Task.Delay(500); // Wait for 0.5 seconds
        }
    }
}
