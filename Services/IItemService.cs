using System.Collections.Generic;
using Store.Database.Models;

namespace Store.Services
{
    public interface IItemService
    {
        bool SaveChanges();

        IEnumerable<Item> GetAllItems();

        Item GetItem(int id);

        void CreateItem(Item item);

        void UpdateItem(Item item);
        
        void DeleteItem(Item item);
    }
}