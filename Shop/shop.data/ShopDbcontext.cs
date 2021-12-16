using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Core;
using Shop.Core.Domain;
using System;
using System.Threading.Tasks;

namespace shop.data
{
    public class ShopDbcontext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public ShopDbcontext(DbContextOptions<ShopDbcontext> options)
            : base(options) { }           
    
        public DbSet<Product> Product { get; set; }

        public DbSet<ExistingFilePath> ExistingFilePath { get; set; }
    }
}
