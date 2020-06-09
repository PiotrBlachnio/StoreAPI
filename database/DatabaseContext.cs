using Microsoft.EntityFrameworkCore;
using Store.Database.Models;

namespace Store.Database
{
    public class DatabaseContext: DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt): base(opt) {}

        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }
    }
}