using System.Collections.Generic;
using Store.Models;

namespace Store.Data 
{
    public interface IStoreRepo
    {
        bool SaveChanges();

        IEnumerable<Item> GetAllItems();
        Item GetItemById(int id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}