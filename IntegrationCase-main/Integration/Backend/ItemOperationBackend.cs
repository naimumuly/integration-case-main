using System.Collections.Concurrent;
using Integration.Common;
using Integration.Data;

namespace Integration.Backend;

public sealed class ItemOperationBackend
{
    
    private int _identitySequence;

    public async Task<Item> SaveItem(string itemContent)
    {
        // This simulates how long it takes to save
        // the item content. Forty seconds, give or take.
        //Thread.Sleep(2000);

        var item = new Item();
        item.Content = itemContent;
        item.Id = GetNextIdentity().Result;
        DataContext.Items.Add(item);

        return await Task.FromResult(item);
    }

    public void RequestSave()
    {
        DataContext.RequestCount++;
    }

    public async Task<List<Item>> FindItemsWithContent(string itemContent)
    {
        return await Task.FromResult(DataContext.Items.Where(x => x.Content == itemContent).ToList());
    }

    private async Task<int> GetNextIdentity()
    {
        return await Task.FromResult(Interlocked.Increment(ref _identitySequence));
    }

    public async Task<List<Item>> GetAllItems()
    {
        return await Task.FromResult(DataContext.Items.OrderBy(k => k.Id).ToList());
    }

    public async Task<int> GetCount()
    {
        return await Task.FromResult(DataContext.Items.Count());
    }

    public async Task<int> GetRequestCount()
    {
        return await Task.FromResult(DataContext.RequestCount);
    }
}