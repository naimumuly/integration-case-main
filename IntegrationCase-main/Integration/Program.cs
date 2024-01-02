using Integration.Service;
using System;

namespace Integration;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var services = new List<ItemIntegrationService>();
        for (int i = 0; i < 100000; i++)
        {
            var newService = new ItemIntegrationService();
            services.Add(newService);
        }

        List<Task> tasks = new List<Task>();

        string[] data = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        Random random = new Random();
        for (int i = 0; i < 1000000; i++)
        {

            int indexAlphabet = random.Next(data.Length);
            int indexService = random.Next(services.Count);

            Task task = services[indexService].SaveItem(data[indexAlphabet]);
            tasks.Add(task);

        }

        Task.WhenAll(tasks);

        var service = new ItemIntegrationService();

        int count = service.GetCount().Result;
        var items = service.GetAllItems().Result.OrderBy(k => k.Content).ToList();
        int callCount = service.GetRequestCount().Result;
        string sjoin = string.Join("", items.Select(a => a.Content));

        Console.WriteLine("Item Count: {0} Request: {1} Unique Items: {2}", count, callCount, sjoin);
        
        Console.ReadLine();
    }
}