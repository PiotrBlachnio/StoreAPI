using Microsoft.EntityFrameworkCore;
using Store.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Store.Database
{
    public class DatabaseContext: IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt): base(opt) {}

        public DbSet<Item> Items { get; set; }
    }
}