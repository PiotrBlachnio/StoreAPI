using Microsoft.EntityFrameworkCore;
using Store.Database.Models;

namespace Store.Database
{
    public class StoreContext: DbContext 
    {
        public StoreContext(DbContextOptions<StoreContext> opt): base(opt) {}

        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }
    }
}