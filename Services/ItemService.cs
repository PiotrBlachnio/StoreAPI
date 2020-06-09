using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Database;
using Store.Database.Models;

namespace Store.Services
{
    public class ItemService : IItemService
    {
        private readonly DatabaseContext _context;

        public ItemService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await _context.Items.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> CreateItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);

            var created = await _context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            _context.Items.Remove(item);

            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }
    }
}