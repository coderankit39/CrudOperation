using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CrudOperation.Models
{
    public class ApplicationDbContext : DbContext   
    {
        internal IEnumerable categories;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
