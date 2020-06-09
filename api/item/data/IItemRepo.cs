using System.Collections.Generic;
using Store.Database.Models;

namespace Store.Data
{
    public interface IItemRepo
    {
        bool SaveChanges();

        IEnumerable<Item> GetAllItems();
        Item GetItem(int id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}