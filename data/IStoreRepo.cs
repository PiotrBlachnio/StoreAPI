using System.Collections.Generic;
using Store.Models;

namespace Store.Data 
{
    public interface IStoreRepo
    {
        bool SaveChanges();

        IEnumerable<Item> GetAllItems();
    }
}