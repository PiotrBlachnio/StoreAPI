using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data
{
    public class StoreContext: DbContext 
    {
        public StoreContext(DbContextOptions<StoreContext> opt): base(opt) {}

        public DbSet<Item> Items { get; set; }
    }
}