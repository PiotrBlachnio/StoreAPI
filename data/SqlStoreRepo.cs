using System;
using System.Collections.Generic;
using System.Linq;
using Store.Models;

namespace Store.Data
{
    public class SqlStoreRepo: IStoreRepo
    {
        private readonly StoreContext _context;

        public SqlStoreRepo(StoreContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }

        public Item GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(p => p.Id == id);
        }

        public void CreateItem(Item item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Items.Add(item);
        }
    }
}