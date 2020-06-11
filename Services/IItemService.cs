using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Database.Models;

namespace Store.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItemsAsync();

        Task<Item> GetItemAsync(Guid id);

        Task<bool> CreateItemAsync(Item item);

        Task<bool> UpdateItemAsync(Item item);
        
        Task<bool> DeleteItemAsync(Item item);

        Task<bool> UserOwnsItemAsync(Guid itemId, string userId);
    }
}