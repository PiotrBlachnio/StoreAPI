using System;
using System.Collections.Generic;
using System.Linq;
using Store.Database;
using Store.Database.Models;

namespace Store.Data
{
    public class SqlItemRepo: IItemRepo
    {
        private readonly StoreContext _context;

        public SqlItemRepo(StoreContext context)
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

        public void UpdateItem(Item item) {}

        public void DeleteItem(Item item)
        {
            if(item == null)
            {
                throw new ArgumentException(nameof(item));
            }

            _context.Items.Remove(item);
        }
    }
}