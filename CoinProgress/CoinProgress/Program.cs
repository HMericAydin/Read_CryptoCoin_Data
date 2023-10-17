using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        float lastPrice = 0;
        float curPrice;
        float initialPrice = CoinSelection("BTCUSDT"); // Get the initial data after starting the process.
        bool entry_exit = true;
        while (entry_exit)
        {
            curPrice = CoinSelection("BTCUSDT");
            if (lastPrice != curPrice)
            {
                if (initialPrice > curPrice * 1.03) // If the profit is 3% stop the process
                {
                    entry_exit = false;
                }
                lastPrice = curPrice;
                priceChanged(lastPrice, initialPrice);
            }
            Thread.Sleep(500); // Wait for 0.5 seconds
        }
    }

    private static float CoinSelection(string crypto) // Select the live coin data
    {
        float price = 0;
        using (var client = new HttpClient())
        {
            var response = client.GetAsync("https://api.binance.com/api/v1/ticker/price?symbol="+crypto).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic data = JsonConvert.DeserializeObject(content);
            price = data.price;
            Console.WriteLine(crypto+ " is " + price);
            Console.WriteLine("#################################");
        }
        return price;
    }

    static void priceChanged(float coin, float init)
    {
        Console.WriteLine("The price changed! = " + coin);
        Console.WriteLine("The price changed! = " + 100*init/coin+"%");
    }
}