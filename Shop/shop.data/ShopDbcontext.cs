using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;


namespace shop.data
{
    public class ShopDbcontext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public ShopDbcontext(DbContextOptions<ShopDbcontext> options)
            : base(options) { }           
    
        public DbSet<Product> Product { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Spaceship> Spaceship { get; set; }

        public DbSet<ExistingFilePath> ExistingFilePath { get; set; }
        public DbSet<FileToDatabase> FileToDatabase { get; set; }
    }
}
