using System.Runtime.Caching;
namespace Caching
{

    class Program
    {
        static void Main()
        {
            // Create a MemoryCache instance
            MemoryCache cache = MemoryCache.Default;

            // Set an item in the cache with a key, value, and optional cache policy
            string key = "exchangeRates";
            decimal[] exchangeRates = GetExchangeRates(); // Your method to fetch exchange rates

            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(3000)//.AddMinutes(30)
                // Set expiration time (30 minutes in this example)
            };

            cache.Add(key, exchangeRates, policy);

            // Retrieve item from the cache
            decimal[]? cachedRates = cache.Get(key) as decimal[];

            if (cachedRates != null)
            {
                // Use cached rates
                Console.WriteLine("Using cached exchange rates");
                foreach (var rate in cachedRates)
                {
                    Console.WriteLine(rate);
                }
            }
            else
            {
                // Fetch from the source and update the cache
                Console.WriteLine("Fetching exchange rates from the source");
                exchangeRates = GetExchangeRates(); // Your method to fetch exchange rates
                cache.Set(key, exchangeRates, policy);
            }
        }

        static decimal[] GetExchangeRates()
        {
            // Your logic to fetch exchange rates from the source (e.g., web service, database, etc.)
            // For simplicity, returning dummy data in this example
            return new decimal[] { 1.2m, 0.8m, 0.5m };
        }
    }

}