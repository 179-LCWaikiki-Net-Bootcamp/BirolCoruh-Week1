using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;

namespace StoreApi.DbOperations
{
        public class StoreDbContext : DbContext
        {
            public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
            { }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }

            public override int SaveChanges()
            {
                return base.SaveChanges();
            }
        }
}
