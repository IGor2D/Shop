using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Core;
using Shop.Core.Domain;

namespace shop.data
{
    public class ShopDbcontext : IdentityDbContext
    {
        public ShopDbcontext(DbContextOptions<ShopDbcontext> options)
            : base(options) { }           
    public DbSet<Product> Product { get; set; }
    }
}
